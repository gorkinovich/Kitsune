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

;---------------------------------------------------------------------------
; Symbols (Memory)
;---------------------------------------------------------------------------

BUFFER_SCREEN    = $0400 ; Default area of screen memory (1000 bytes).
BUFFER_COLORS    = $D800 ; Color RAM (1000 bytes, only bits #0-#3).

SCREEN_POINTER   = $00D1 ; [$00D1-$00D2] Pointer to current line in screen memory.
COLORS_POINTER   = $00F3 ; [$00F3-$00F4] Pointer to current line in Color RAM.
SCREEN_LINES     = $00D9 ; [$00D9-$00F1] High byte of pointers to each line in screen memory (25 bytes).
CURSOR_COLUMN    = $00D3 ; Current cursor column. Values: $00-$27, 0-39.
CURSOR_ROW       = $00D6 ; Current cursor row. Values: $00-$18, 0-24.

CURSOR_COLOR     = $0286 ; Current color, cursor color. Values: $00-$0F, 0-15.
CURUND_COLOR     = $0287 ; Color of character under cursor. Values: $00-$0F, 0-15.
FRAME_COLOR      = $D020 ; Border color (only bits #0-#3).
BACKGROUND_COLOR = $D021 ; Background color (only bits #0-#3).
BACKEXTRA1_COLOR = $D022 ; Extra background color #1 (only bits #0-#3).
BACKEXTRA2_COLOR = $D023 ; Extra background color #2 (only bits #0-#3).
BACKEXTRA3_COLOR = $D024 ; Extra background color #3 (only bits #0-#3).

KEY_MATRIX_CODE  = $00CB ; Matrix code of key currently being pressed:
                         ; $00-$3F = Keyboard matrix code
                         ; $40     = No key pressed
KEY_SHIFT_STATUS = $028D ; Shift key indicator:
                         ; Bit #0: 1 = Shift keys pressed
                         ; Bit #1: 1 = Commodore key pressed
                         ; Bit #2: 1 = Control key pressed
KEY_STOP_STATUS  = $0091 ; Stop key indicator:
                         ; $7F = Stop key is pressed.
                         ; $FF = Stop key is not pressed.

DEVICE_INPUT     = $0099 ; Current input device number. Default: $00, keyboard.
DEVICE_OUTPUT    = $009A ; Current output device number. Default: $03, screen.
TIME_OF_DAY      = $00A0 ; [$00A0-$00A2] Time of day (TI variable), +1 every 1/60 second (PAL).
SYSREGA          = $030C ; Default value of register A for SYS.
SYSREGX          = $030D ; Default value of register X for SYS.
SYSREGY          = $030E ; Default value of register Y for SYS.
SYSREGST         = $030F ; Default value of status register for SYS.

;---------------------------------------------------------------------------
; Symbols (Unused)
;---------------------------------------------------------------------------

UNUSED_DIR01 = $0002 ; (01 BYTE ) $0002
UNUSED_DIR02 = $0003 ; (02 BYTES) $0003-$0004
UNUSED_DIR03 = $0005 ; (02 BYTES) $0005-$0006
UNUSED_DIR04 = $002A ; (01 BYTE ) $002A
UNUSED_DIR05 = $0052 ; (01 BYTE ) $0052
UNUSED_DIR06 = $00FB ; (04 BYTES) $00FB-$00FE
UNUSED_DIR11 = $02A7 ; (89 BYTES) $02A7-$02FF 
UNUSED_DIR12 = $0313 ; (01 BYTE ) $0313
UNUSED_DIR13 = $032E ; (02 BYTES) $032E-$032F
UNUSED_DIR14 = $0334 ; (08 BYTES) $0334-$033B
UNUSED_DIR15 = $03FC ; (04 BYTES) $03FC-$03FF
UNUSED_DIR16 = $07E8 ; (16 BYTES) $07E8-$07F7
UNUSED_DIR17 = $DBE8 ; (24 BYTES) $DBE8-$DBFF

;---------------------------------------------------------------------------
; Symbols (Colors)
;---------------------------------------------------------------------------

COLOR_BLACK     = $00 ; Black
COLOR_WHITE     = $01 ; White
COLOR_RED       = $02 ; Red
COLOR_CYAN      = $03 ; Cyan
COLOR_PURPLE    = $04 ; Purple
COLOR_GREEN     = $05 ; Green
COLOR_BLUE      = $06 ; Blue
COLOR_YELLOW    = $07 ; Yellow
COLOR_ORANGE    = $08 ; Orange
COLOR_BROWN     = $09 ; Brown
COLOR_LRED      = $0A ; Light Red
COLOR_DGREY     = $0B ; Dark Grey
COLOR_GREY      = $0C ; Grey
COLOR_LGREEN    = $0D ; Light Green
COLOR_LBLUE     = $0E ; Light Blue
COLOR_LGREY     = $0F ; Light Grey

MAX_COLORS      = $10 ; Number of colors

;---------------------------------------------------------------------------
; Symbols (Characters)
;---------------------------------------------------------------------------

CH_AT           = $00
CH_A            = $01
CH_B            = $02
CH_C            = $03
CH_D            = $04
CH_E            = $05
CH_F            = $06
CH_G            = $07
CH_H            = $08
CH_I            = $09
CH_J            = $0A
CH_K            = $0B
CH_L            = $0C
CH_M            = $0D
CH_N            = $0E
CH_O            = $0F
CH_P            = $10
CH_Q            = $11
CH_R            = $12
CH_S            = $13
CH_T            = $14
CH_U            = $15
CH_V            = $16
CH_W            = $17
CH_X            = $18
CH_Y            = $19
CH_Z            = $1A
CH_LBRACKET     = $1B
CH_POUND        = $1C
CH_RBRACKET     = $1D
CH_UP           = $1E
CH_RIGHT        = $1F
CH_SPACE        = $20
CH_EXCLAMATION  = $21
CH_DQUOTES      = $22
CH_SHARP        = $23
CH_DOLAR        = $24
CH_PERCENT      = $25
CH_AMPERSAND    = $26
CH_ACCENT       = $27
CH_LPARENTHESES = $28
CH_RPARENTHESES = $29
CH_ASTERISK     = $2A
CH_PLUS         = $2B
CH_COMMA        = $2C
CH_MINUS        = $2D
CH_PERIOD       = $2E
CH_SLASH        = $2F
CH_D0           = $30
CH_D1           = $31
CH_D2           = $32
CH_D3           = $33
CH_D4           = $34
CH_D5           = $35
CH_D6           = $36
CH_D7           = $37
CH_D8           = $38
CH_D9           = $39
CH_COLON        = $3A
CH_SEMICOLON    = $3B
CH_LESS         = $3C
CH_EQUAL        = $3D
CH_GREATER      = $3E
CH_QUESTION     = $3F
CH_HORIZONTAL   = $40
CH_SPADE        = $41
CH_VERTICAL     = $42
CH_LINE_HC      = $43
CH_LINE_HCN1    = $44
CH_LINE_HCN2    = $45
CH_LINE_HCS1    = $46
CH_LINE_VCW1    = $47
CH_LINE_VCE1    = $48
CH_CURVE_NE     = $49
CH_CURVE_SW     = $4A
CH_CURVE_SE     = $4B
CH_EDGE_SW      = $4C
CH_DIAGONAL_WE  = $4D
CH_DIAGONAL_EW  = $4E
CH_EDGE_NW      = $4F
CH_EDGE_NE      = $50
CH_CIRCLE       = $51
CH_LINE_HCS2    = $52
CH_HEART        = $53
CH_LINE_VCW2    = $54
CH_CURVE_NW     = $55
CH_DCROSS       = $56
CH_DONUT        = $57
CH_CLOVER       = $58
CH_LINE_VCE2    = $59
CH_DIAMOND      = $5A
CH_CROSS        = $5B
CH_HFLAG_W      = $5C
CH_LINE_VC      = $5D
CH_PI           = $5E
CH_HALF_TWB     = $5F
CH_BLANK        = $60
CH_HHALVES      = $61
CH_VHALVES      = $62
CH_LINE_VN      = $63
CH_LINE_VS      = $64
CH_LINE_HW      = $65
CH_FLAG         = $66
CH_LINE_HE      = $67
CH_HFLAG_S      = $68
CH_HALF_TBW     = $69
CH_EDGE_E       = $6A
CH_TSHAPE_W     = $6B
CH_CH_SQUARE_SE = $6C
CH_CORNER_SW    = $6D
CH_CORNER_NE    = $6E
CH_EDGE_S       = $6F
CH_CORNER_NW    = $70
CH_TSHAPE_S     = $71
CH_TSHAPE_N     = $72
CH_TSHAPE_E     = $73
CH_EDGE_W       = $74
CH_EDGE2_W      = $75
CH_EDGE2_E      = $76
CH_EDGE_N       = $77
CH_EDGE2_N      = $78
CH_EDGE2_S      = $79
CH_CORNER_SE    = $7A
CH_SQUARE_SW    = $7B
CH_SQUARE_NE    = $7C
CH_CORNER_SE    = $7D
CH_SQUARE_NW    = $7E
CH_FLAG2        = $7F

;---------------------------------------------------------------------------
; Symbols (Characters Inverted)
;---------------------------------------------------------------------------

CI_AT           = $80
CI_A            = $81
CI_B            = $82
CI_C            = $83
CI_D            = $84
CI_E            = $85
CI_F            = $86
CI_G            = $87
CI_H            = $88
CI_I            = $89
CI_J            = $8A
CI_K            = $8B
CI_L            = $8C
CI_M            = $8D
CI_N            = $8E
CI_O            = $8F
CI_P            = $90
CI_Q            = $91
CI_R            = $92
CI_S            = $93
CI_T            = $94
CI_U            = $95
CI_V            = $96
CI_W            = $97
CI_X            = $98
CI_Y            = $99
CI_Z            = $9A
CI_LBRACKET     = $9B
CI_POUND        = $9C
CI_RBRACKET     = $9D
CI_UP           = $9E
CI_RIGHT        = $9F
CI_SPACE        = $A0
CI_EXCLAMATION  = $A1
CI_DQUOTES      = $A2
CI_SHARP        = $A3
CI_DOLAR        = $A4
CI_PERCENT      = $A5
CI_AMPERSAND    = $A6
CI_ACCENT       = $A7
CI_LPARENTHESES = $A8
CI_RPARENTHESES = $A9
CI_ASTERISK     = $AA
CI_PLUS         = $AB
CI_COMMA        = $AC
CI_MINUS        = $AD
CI_PERIOD       = $AE
CI_SLASH        = $AF
CI_D0           = $B0
CI_D1           = $B1
CI_D2           = $B2
CI_D3           = $B3
CI_D4           = $B4
CI_D5           = $B5
CI_D6           = $B6
CI_D7           = $B7
CI_D8           = $B8
CI_D9           = $B9
CI_COLON        = $BA
CI_SEMICOLON    = $BB
CI_LESS         = $BC
CI_EQUAL        = $BD
CI_GREATER      = $BE
CI_QUESTION     = $BF
CI_HORIZONTAL   = $C0
CI_SPADE        = $C1
CI_VERTICAL     = $C2
CI_LINE_HC      = $C3
CI_LINE_HCN1    = $C4
CI_LINE_HCN2    = $C5
CI_LINE_HCS1    = $C6
CI_LINE_VCW1    = $C7
CI_LINE_VCE1    = $C8
CI_CURVE_NE     = $C9
CI_CURVE_SW     = $CA
CI_CURVE_SE     = $CB
CI_EDGE_SW      = $CC
CI_DIAGONAL_WE  = $CD
CI_DIAGONAL_EW  = $CE
CI_EDGE_NW      = $CF
CI_EDGE_NE      = $D0
CI_CIRCLE       = $D1
CI_LINE_HCS2    = $D2
CI_HEART        = $D3
CI_LINE_VCW2    = $D4
CI_CURVE_NW     = $D5
CI_DCROSS       = $D6
CI_DONUT        = $D7
CI_CLOVER       = $D8
CI_LINE_VCE2    = $D9
CI_DIAMOND      = $DA
CI_CROSS        = $DB
CI_HFLAG_W      = $DC
CI_LINE_VC      = $DD
CI_PI           = $DE
CI_HALF_TWB     = $DF
CI_BLANK        = $E0
CI_HHALVES      = $E1
CI_VHALVES      = $E2
CI_LINE_VN      = $E3
CI_LINE_VS      = $E4
CI_LINE_HW      = $E5
CI_FLAG         = $E6
CI_LINE_HE      = $E7
CI_HFLAG_S      = $E8
CI_HALF_TBW     = $E9
CI_EDGE_E       = $EA
CI_TSHAPE_W     = $EB
CI_CI_SQUARE_SE = $EC
CI_CORNER_SW    = $ED
CI_CORNER_NE    = $EE
CI_EDGE_S       = $EF
CI_CORNER_NW    = $F0
CI_TSHAPE_S     = $F1
CI_TSHAPE_N     = $F2
CI_TSHAPE_E     = $F3
CI_EDGE_W       = $F4
CI_EDGE2_W      = $F5
CI_EDGE2_E      = $F6
CI_EDGE_N       = $F7
CI_EDGE2_N      = $F8
CI_EDGE2_S      = $F9
CI_CORNER_SE    = $FA
CI_SQUARE_SW    = $FB
CI_SQUARE_NE    = $FC
CI_CORNER_SE    = $FD
CI_SQUARE_NW    = $FE
CI_FLAG2        = $FF
