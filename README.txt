                                  vvvvvv
                               >> README <<
                                  ^^^^^^

i.   Penjelasan Singkat Algortima
     >> Untuk fitur rekomendasi, dicatat semua teman dari akun,
        Setelah itu, akan dicek teman dari semua akun lain yang
        bukan teman dari akun tersebut untuk menghitung jumlah
        mutual friend yang mereka punya sambil mencatat akun
        mana yang merupakan mutual friend. Kemudian, akun-akun
        tersebut akan diurutkan dan ditampilkan hasilnya.
     >> Untuk fitur explore, terdapat 2 algoritma, yaitu BFS
        dan DFS. Untuk keduanya, simpul dicari berdasarkan urutan
        alfabetis. Pada DFS, program akan memeriksa satu simpul
        yang bertetangga dengan simpul awal terlebih dahulu, misal
        simpul w, lalu mengulangi proses pencariannya dengan
        mengunjungi tetangga simpul w. Pada BFS, program akan
        mengunjungi semua simpul bertetangga terlebih dahulu,
        lalu mengunjungi semua simpul yang bertetangga dengan
        simpul yang dikunjungi sebelumnya.


ii.  Requirement Program
        >> Sistem operasi Windows.


iii. Cara Menggunakan
     >> Menjalankan Program
        Buka file "Socialink.exe" dalam folder /bin.

     >> Memasukkan Input
        Gunakan button Browse untuk memilih file input .txt.
        
     >> Format File Input
        File input ditulis dengan format seperti contoh berikut.
        6
        Budi Siti
        Doni Eko
        Siti Eko
        Feli Budi
        Budi Ali
        Gio Heri
        
        (Keterangan: Angka paling atas adalah jumlah sisi pada graf.
                     Berikutnya ditulis nama yang akan menjadi simpul
                     dengan temannya yang dipisah dengan satu spasi.
                     Koneksi berikutnya ditulis setelah newline.
                     
     >> Pengaturan
        Terdapat opsi untuk memilih algoritma yang digunakan. Pilihlah
        algoritma BFS untuk melakukan explore dengan BFS dan DFS untuk
        melakukan explore dengan DFS.

iv.  Author
     >> Nama Kel: Socialink
        Anggota : 13519063 Melita
                  13519070 Mhd. Hiro Agayeff Muslion
                  13519171 Fauzan Yubairi Indrayadi
