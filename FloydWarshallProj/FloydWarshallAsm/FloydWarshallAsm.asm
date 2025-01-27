.data
MaxValue dd 7FFFFFFFh

;1 arg = RCX - row number
; 2arg = RDX - number of vertices in graph
;3 arg = R8 - address where there is the first table elem
.code 
InitializeRowAsm proc 
VPBROADCASTD xmm1, [MaxValue] 

    mov RAX, RDX
    mov R10, 0                  ; potem bedziemy za pomoca rejestru R10 sprawdzali czy juz w rzedzie jest tyle elementow ile wierzcholkow w grafie
    mov r9, r8                 ; dajemy do r9 tez adres bo potem bedziemy za pomoca adresu poczatku tablicy obliczac miejsce w pamieci gdzie bedzie 0
    
InitializeLoop:
    movaps [r8], xmm1
    add R10, 4
    add r8, 16
    sub RAX, R10
    JLE End_InitializeRow
    jmp InitializeLoop            ; Powróæ do pocz¹tku pêtli

End_InitializeRow:
    mov RAX, R9   ;bedziemy zwracac wskaznik do rzedu - ej ale to rownie dobrze moze chyba na stosie byc bo potem w c# bedzie niszczone - chociaz trzeba sie zastanwoic
    mov DWORD PTR [R9+4*RCX], 0

ret 
InitializeRowAsm endp 

CalculateRowForKAsm proc
sub RCX, RDX 
mov RAX, RCX 
ret 
CalculateRowForKAsm endp 

end 