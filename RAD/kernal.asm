;================================================================================
; Copyright (c) 2023 Gorka Suárez García
;
; Permission is hereby granted, free of charge, to any person obtaining a copy
; of this software and associated documentation files (the "Software"), to deal
; in the Software without restriction, including without limitation the rights
; to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
; copies of the Software, and to permit persons to whom the Software is
; furnished to do so, subject to the following conditions:
;
; The above copyright notice and this permission notice shall be included in
; all copies or substantial portions of the Software.
;
; THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
; IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
; FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
; AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
; LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
; OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
; SOFTWARE.
;================================================================================

;***************************************************************************
; KERNAL (Screen)
;***************************************************************************

;---------------------------------------------------------------------------
; Initialize VIC; restore default input/output to keyboard/screen;
; clear screen; set PAL/NTSC switch and interrupt timer.
;---------------------------------------------------------------------------

defm KERNAL_SCINIT
        jsr $FF5B
endm

;---------------------------------------------------------------------------
; Fetch number of screen rows and columns.
;---------------------------------------------------------------------------

defm KERNAL_SCREEN      ; [out] cols, [out] rows
        jsr $E505
        stx /1
        sty /2
endm

;---------------------------------------------------------------------------
; Save or restore cursor position.
;---------------------------------------------------------------------------

defm KERNAL_PLOT_W      ; col, row
        clc
        ldx #/1
        ldy #/2
        jsr $E50A
endm

defm KERNAL_PLOT_R      ; [out] col, [out] row
        sec
        jsr $E50A
        stx /1
        sty /2
endm

;---------------------------------------------------------------------------
; Set system error display switch at memory address $009D.
;---------------------------------------------------------------------------

defm KERNAL_SETMSG      ; flag
        lda #/1         ; A -> bit7=1 error messages on
                        ; A -> bit6=1 control messages on
        jsr $FE18
endm

;***************************************************************************
; KERNAL (Keyboard)
;***************************************************************************

;---------------------------------------------------------------------------
; Read byte from default input. (If not keyboard, call OPEN and CHKIN
; before.)
;---------------------------------------------------------------------------

defm KERNAL_GETIN       ; -> A
        jsr $F13E
endm

defm KERNAL_GETIN_1     ; [out] variable
        jsr $F13E
        sta /1
endm

;---------------------------------------------------------------------------
; Query Stop key indicator, at memory address $0091; if pressed,
; call CLRCHN and clear keyboard buffer.
;---------------------------------------------------------------------------

defm KERNAL_STOP        ; -> Flags(Z,C)
        jsr $F6ED
endm

;---------------------------------------------------------------------------
; Query keyboard; put current matrix code into memory address $00CB,
; current status of shift keys into memory address $028D and PETSCII
; code into keyboard buffer. (Called by the default IRQ.)
;---------------------------------------------------------------------------

defm KERNAL_SCNKEY
        jsr $EA87
endm

;***************************************************************************
; KERNAL (Time)
;***************************************************************************

;---------------------------------------------------------------------------
; Set Time of Day, at memory address $00A0-$00A2.
;---------------------------------------------------------------------------

defm KERNAL_SETTIM      ; v0, v1, v2
        lda #/1
        ldx #/2
        ldy #/3
        jsr $F6E4
endm

;---------------------------------------------------------------------------
; Read Time of Day, at memory address $00A0-$00A2.
;---------------------------------------------------------------------------

defm KERNAL_RDTIM       ; d0, d1, d2
        jsr $F6DD
        sta /1
        stx /2
        sty /3
endm

;---------------------------------------------------------------------------
; Update Time of Day, at memory address $00A0-$00A2, and Stop key
; indicator, at memory address $0091.
;---------------------------------------------------------------------------

defm KERNAL_UDTIM
        jsr $F69B
endm

;***************************************************************************
; KERNAL (Files)
;***************************************************************************

; OPEN --------------------+
;  CHKIN/CHKOUT ---------+ |
;    begin a loop -----+ | |
;      input or output | | |
;    end a loop -------+ | |
;  CLRCHN ---------------+ |
; CLOSE -------------------+

;---------------------------------------------------------------------------
; Set file parameters.
;---------------------------------------------------------------------------

defm KERNAL_SETLFS      ; id, device, mode
        lda #/1
        ldx #/2
        ldy #/3
        jsr $FE00
endm

;---------------------------------------------------------------------------
; Set file name parameters.
;---------------------------------------------------------------------------

defm KERNAL_SETNAM      ; size, string
        lda #/1
        ldx #</2        ; Low byte of the pointer
        ldy #>/2        ; High byte of the pointer
        jsr $FDF9
endm

;---------------------------------------------------------------------------
; Open file. (Call SETLFS and SETNAM before.)
;---------------------------------------------------------------------------

defm KERNAL_OPEN
        jsr $F34A
endm

;---------------------------------------------------------------------------
; Close file.
;---------------------------------------------------------------------------

defm KERNAL_CLOSE ; id
        lda #/1
        jsr $F291
endm

;---------------------------------------------------------------------------
; Load or verify file. (Call SETLFS and SETNAM before.)
;---------------------------------------------------------------------------

defm KERNAL_LOAD        ; mode, pointer, [out] variable
        lda #/1
        ldx #</2        ; Low byte of the pointer
        ldy #>/2        ; High byte of the pointer
        jsr $F49E
        bcc @ok
        sta /3
        jmp @end
@ok     stx /3
        sty /3+1
@end    nop
endm

;---------------------------------------------------------------------------
; Save file. (Call SETLFS and SETNAM before.)
;---------------------------------------------------------------------------

defm KERNAL_SAVE        ; zpdir, pointer, [out] variable
        lda #/1
        ldx #</2        ; Low byte of the pointer
        ldy #>/2        ; High byte of the pointer
        jsr $F5DD
        bcs @end
        sta #0
@end    sta /3
endm

;***************************************************************************
; KERNAL (I/O)
;***************************************************************************

;---------------------------------------------------------------------------
; Define file as default input. (Call OPEN before.)
;---------------------------------------------------------------------------

defm KERNAL_CHKIN       ; id
        ldx #/1
        jsr $F20E
endm

;---------------------------------------------------------------------------
; Define file as default output. (Call OPEN before.)
;---------------------------------------------------------------------------

defm KERNAL_CHKOUT      ; id
        ldx #/1
        jsr $F250
endm

;---------------------------------------------------------------------------
; Close default input/output files (for serial bus, send UNTALK and/or
; UNLISTEN); restore default input/output to keyboard/screen.
;---------------------------------------------------------------------------

defm KERNAL_CLRCHN
        jsr $F333
endm

;---------------------------------------------------------------------------
; Fetch status of current input/output device, value of ST variable.
; (For RS232, status is cleared.)
;---------------------------------------------------------------------------

defm KERNAL_READST      ; -> A
        jsr $FE07
endm

defm KERNAL_READST_1    ; [out] variable
        jsr $FE07
        sta /1
endm

;---------------------------------------------------------------------------
; Read byte from default input (for keyboard, read a line from the screen).
; (If not keyboard, call OPEN and CHKIN before.)
;---------------------------------------------------------------------------

defm KERNAL_CHRIN       ; -> A
        jsr $F157
endm

defm KERNAL_CHRIN_1     ; [out] variable
        jsr $F157
        sta /1
endm

;---------------------------------------------------------------------------
; Write byte to default output. (If not screen, call OPEN and CHKOUT
; before.)
;---------------------------------------------------------------------------

defm KERNAL_CHROUT      ; value
        lda #/1
        jsr $F1CA
endm

;---------------------------------------------------------------------------
; Clear file table; call CLRCHN.
;---------------------------------------------------------------------------

defm KERNAL_CLALL
        jsr $F32F
endm

;---------------------------------------------------------------------------
; Initialize CIA's, SID volume; setup memory configuration;
; set and start interrupt timer.
;---------------------------------------------------------------------------

defm KERNAL_IOINIT
        jsr $FDA3
endm

;***************************************************************************
; KERNAL (IEC)
;***************************************************************************

;---------------------------------------------------------------------------
; Send TALK command to serial bus.
;---------------------------------------------------------------------------

defm KERNAL_TALK        ; device
        lda #/1
        jsr $ED09
endm

;---------------------------------------------------------------------------
; Send TALK secondary address to serial bus. (Call TALK before.)
;---------------------------------------------------------------------------

defm KERNAL_TALKSA      ; second_dir
        lda #/1
        jsr $EDC7
endm

;---------------------------------------------------------------------------
; Send UNTALK command to serial bus.
;---------------------------------------------------------------------------

defm KERNAL_UNTALK
        jsr $EDEF
endm

;---------------------------------------------------------------------------
; Read byte from serial bus. (Call TALK and TALKSA before.)
;---------------------------------------------------------------------------

defm KERNAL_IECIN       ; -> A
        jsr $EE13
endm

defm KERNAL_IECIN_1     ; [out] variable
        jsr $EE13
        sta /1
endm

;---------------------------------------------------------------------------
; Send LISTEN command to serial bus.
;---------------------------------------------------------------------------

defm KERNAL_LISTEN      ; device
        lda #/1
        jsr $EDFE
endm

;---------------------------------------------------------------------------
; Send LISTEN secondary address to serial bus. (Call LISTEN before.)
;---------------------------------------------------------------------------

defm KERNAL_LSTNSA      ; second_dir
        lda #/1
        jsr $EDB9
endm

;---------------------------------------------------------------------------
; Send UNLISTEN command to serial bus.
;---------------------------------------------------------------------------

defm KERNAL_UNLSTN
        jsr $EDFE
endm

;---------------------------------------------------------------------------
; Write byte to serial bus. (Call LISTEN and LSTNSA before.)
;---------------------------------------------------------------------------

defm KERNAL_IECOUT      ; value
        lda #/1
        jsr $EDDD
endm

;---------------------------------------------------------------------------
; Unknown. (Set serial bus timeout.)
;---------------------------------------------------------------------------

defm KERNAL_SETTMO      ; timeout
        lda #/1
        jsr $FE21
endm

;***************************************************************************
; KERNAL (Memory)
;***************************************************************************

;---------------------------------------------------------------------------
; Save or restore start address of BASIC work area.
;---------------------------------------------------------------------------

defm KERNAL_MEMBOT_W    ; pointer
        clc
        ldx #</1        ; Low byte of the pointer
        ldy #>/1        ; High byte of the pointer
        jsr $FE25
endm

defm KERNAL_MEMBOT_R    ; [out] pointer
        sec
        jsr $FE25
        stx #</1        ; Low byte of the pointer
        sty #>/1        ; High byte of the pointer
endm

;---------------------------------------------------------------------------
; Save or restore end address of BASIC work area.
;---------------------------------------------------------------------------

defm KERNAL_MEMTOP_W    ; pointer
        clc
        ldx #</1        ; Low byte of the pointer
        ldy #>/1        ; High byte of the pointer
        jsr $FE34
endm

defm KERNAL_MEMTOP_R    ; [out] pointer
        sec
        jsr $FE34
        stx #</1        ; Low byte of the pointer
        sty #>/1        ; High byte of the pointer
endm

;---------------------------------------------------------------------------
; Clear memory addresses $0002-$0101 and $0200-$03FF; run memory test and
; set start and end address of BASIC work area accordingly; set screen
; memory to $0400 and datasette buffer to $033C.
;---------------------------------------------------------------------------

defm KERNAL_RAMTAS
        jsr $FD50
endm

;---------------------------------------------------------------------------
; Copy vector table at memory addresses $0314-$0333 from or into user table.
;---------------------------------------------------------------------------

defm KERNAL_VECTOR_W    ; pointer
        clc
        ldx #</1        ; Low byte of the pointer
        ldy #>/1        ; High byte of the pointer
        jsr $FD1A
endm

defm KERNAL_VECTOR_R    ; pointer
        sec
        ldx #</1        ; Low byte of the pointer
        ldy #>/1        ; High byte of the pointer
        jsr $FD1A
endm

;---------------------------------------------------------------------------
; Fill vector table at memory addresses $0314-$0333 with default values.
;---------------------------------------------------------------------------

defm KERNAL_RESTOR
        jsr $FD15
endm

;---------------------------------------------------------------------------
; Fetch CIA #1 base address.
;---------------------------------------------------------------------------

defm KERNAL_IOBASE      ; -> X/Y (low/high)
        jsr $E500
endm

defm KERNAL_IOBASE_1    ; [out] variable
        jsr $E500
        stx /1
        sty /1+1
endm
