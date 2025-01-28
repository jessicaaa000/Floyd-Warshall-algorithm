.data
MaxValue dd 0FFFFFFFh

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
    mov RAX, R9   ;bedziemy zwracac wskaznik do rzedu
    mov DWORD PTR [R9+4*RCX], 0

ret 
InitializeRowAsm endp 

;1 arg = row address - RCX
;2 arg = kRow address - RDX,
;3 arg = k - R8
;4 arg = vertices - R9
;5 address
CalculateRowForKAsm proc
mov r10, [rsp + 40] ; bierzemy 5 arg ze stosu, czyli address
mov r11d, [RCX+4*R8]  ;tu jest od teraz nasza wartosc przez ktora sprawdzamy krotsze sciezki (row[k])
movups xmm1, [RDX]  ;zapisujemy do xmm1 rzad, ktory bedzie sluzyc do obliczania posrednich wierzcholkow
movd xmm2, r11d
VPBROADCASTD xmm2, xmm2 ;wartosc przez ktora sprawdzamy krotsze odcinki
addps xmm2, xmm1   ; teraz w xmm2 mamy sumy odcinkow przez posredni wierzcholek
movups xmm3, [RCX]  ;zapisujemy teraz do xmm3 wierzcholek, ktory bedzie zmieniany
;cmpunordps xmm3, xmm3
minps xmm3, xmm2
movaps [r10], xmm3
mov rax, [r10]

ret 
CalculateRowForKAsm endp 

end 