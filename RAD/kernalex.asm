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
; Screen
;***************************************************************************

;---------------------------------------------------------------------------
; Put current color, at memory address $0286, into color RAM, pointed at by memory addresses $00F3-$00F4.
;---------------------------------------------------------------------------

defm SCREEN_PUTCOLOR    ; col
        ldx #/1
        jsr $E4DA
endm

;---------------------------------------------------------------------------
; Initialize VIC; restore default input/output to keyboard/screen; clear
; screen.
;---------------------------------------------------------------------------
defm SCREEN_IVIOCLS
        jsr $E518
endm

;---------------------------------------------------------------------------
; Clear screen.
;---------------------------------------------------------------------------

defm SCREEN_CLRSCR
        jsr $E544
endm

;---------------------------------------------------------------------------
; Move cursor home, to upper left corner of screen.
;---------------------------------------------------------------------------

defm SCREEN_GOHOME
        jsr $E566
endm

;---------------------------------------------------------------------------
; Set pointer at memory addresses $00D1-$00D2 to current line in screen
; memory and pointer at memory addresses $00F3-$00F4 to current line in
; Color RAM, according to current cursor row, at memory address $00D6,
; and column, at memory address $00D3.
;---------------------------------------------------------------------------

defm SCREEN_GETINFO
        jsr $E56C
endm

;---------------------------------------------------------------------------
; Initialize VIC; restore default input/output to keyboard/screen; move
; cursor home.
;---------------------------------------------------------------------------

defm SCREEN_IVIOGHM
        jsr $E59A
endm

;---------------------------------------------------------------------------
; Initialize VIC; restore default input/output to keyboard/screen.
;---------------------------------------------------------------------------

defm SCREEN_INITVIO
        jsr $E5A0
endm

;---------------------------------------------------------------------------
; Initialize VIC.
;---------------------------------------------------------------------------

defm SCREEN_INITVIC
        jsr $E5A8
endm

;---------------------------------------------------------------------------
; Read byte from screen; if input line is empty, the cursor appears and a
; line of data is input.
;---------------------------------------------------------------------------

defm SCREEN_GETBYTE     ; -> A
        jsr $E632
endm

;---------------------------------------------------------------------------
; Check PETSCII code; if $22, quotation mark, then toggle quotation mode
; switch, at memory address $00D4.
;---------------------------------------------------------------------------

defm SCREEN_CHKQMARK
        jsr $E684
endm

;---------------------------------------------------------------------------
; Recompute the high bytes of pointers to lines in screen memory, at
; memory addresses $00D9-$00F1.
;---------------------------------------------------------------------------

defm SCREEN_UPDLPTR
        jsr $E6B6
endm

;---------------------------------------------------------------------------
; Write byte to screen.
;---------------------------------------------------------------------------

defm SCREEN_PUTBYTE ; value
        lda #/1
        jsr $E716
endm

;---------------------------------------------------------------------------
; Check PETSCII code; if belongs to a color, set current color, at memory
; address $0286.
;---------------------------------------------------------------------------

defm SCREEN_CHKCODE
        jsr $E8CB
endm

;---------------------------------------------------------------------------
; Scroll complete screen upwards.
;---------------------------------------------------------------------------

defm SCREEN_SCROLLUP
        jsr $E8EA
endm

;---------------------------------------------------------------------------
; Insert line before current line and scroll lower part of screen downwards.
;---------------------------------------------------------------------------

defm SCREEN_INSLINE
        jsr $E965
endm

;---------------------------------------------------------------------------
; Set pointer at memory addresses $00D1-$00D2 to current line in screen
; memory, fetching high byte from table at memory addresses $00D9-$00F1.
;---------------------------------------------------------------------------

defm SCREEN_GETSLPTR ; row
        lda #/1
        jsr $E9F0
endm

;---------------------------------------------------------------------------
; Clear screen line.
;---------------------------------------------------------------------------

defm SCREEN_CLRLINE ; row
        lda #/1
        jsr $E9FF
endm

;---------------------------------------------------------------------------
; Write character and color onto screen; set cursor phase delay to 2.
;---------------------------------------------------------------------------

defm SCREEN_PUTCHAR ; char, color
        lda #/1
        ldx #/2
        jsr $EA13
endm

;---------------------------------------------------------------------------
; Set pointer at memory addresses $00F3-$00F4 to current line in Color
; RAM, according to pointer at memory addresses $00D1-$00D2 to current
; line in screen memory.
;---------------------------------------------------------------------------

defm SCREEN_GETCLPTR
        jsr $EA24
endm

;***************************************************************************
; Screen (KERNAL)
;***************************************************************************

;---------------------------------------------------------------------------
; Fetch number of screen rows and columns.
;---------------------------------------------------------------------------

defm SCREEN_GET_SIZE     ; -> X (cols), Y (rows)
        jsr $E505
endm

defm SCREEN_GETM_SIZE    ; [out] cols, [out] rows
        jsr $E505
        stx /1
        sty /2
endm

;---------------------------------------------------------------------------
; Save or restore cursor position.
;---------------------------------------------------------------------------

defm SCREEN_SET_CURSOR  ; col, row
        clc
        ldx #/1
        ldy #/2
        jsr $E50A
endm

defm SCREEN_GET_CURSOR  ; -> X (col), Y (row)
        sec
        jsr $E50A
endm

defm SCREEN_SETM_CURSOR ; col, row
        clc
        ldx /1
        ldy /2
        jsr $E50A
endm

defm SCREEN_GETM_CURSOR ; [out] col, [out] row
        sec
        jsr $E50A
        stx /1
        sty /2
endm

;***************************************************************************
; Keyboard
;***************************************************************************

;---------------------------------------------------------------------------
; Read byte from keyboard buffer; shift keyboard buffer; decrease buffer
; pointer.
;---------------------------------------------------------------------------

defm KEYBOARD_READ      ; -> A
        jsr $E5B4
endm

;---------------------------------------------------------------------------
; Read byte from keyboard buffer; shift keyboard buffer; decrease buffer
; pointer.
;---------------------------------------------------------------------------

defm KEYBOARD_READZ     ; -> A
        jsr $F142
endm

;---------------------------------------------------------------------------
; Update Stop key indicator, at memory address $0091.
;---------------------------------------------------------------------------

defm KEYBOARD_UPDSTP
        jsr $F6BC
endm

;***************************************************************************
; Keyboard (KERNAL)
;***************************************************************************

;---------------------------------------------------------------------------
; Query Stop key indicator, at memory address $0091; if pressed, call
; CLRCHN and clear keyboard buffer.
;---------------------------------------------------------------------------

defm KEYBOARD_STOP      ; -> Flags(Z,C)
        jsr $F6ED
endm

;---------------------------------------------------------------------------
; Query keyboard; put current matrix code into memory address $00CB,
; current status of shift keys into memory address $028D and PETSCII
; code into keyboard buffer; handle Commodore-Shift; repeat keys.
;---------------------------------------------------------------------------

defm KEYBOARD_SCNKEY
        jsr $EA87
endm
