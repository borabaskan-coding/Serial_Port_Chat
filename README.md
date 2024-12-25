# Serial_Port_Chat
 
Bu proje seri port ile c# üzerinden Visual Studio'da oluşturulmuştur.

1- Başlamadan once; sanal COM port çiftlerini oluşturmak için com0com (veya benzeri) cihaz emülatörünü yüklemek gereklidir. Ayrica port adları "COM" seklinde başlamalıdır. emülatörden RX ve TX çaprazlama bağlanmıştır.

2- Öncelikle bir form dosyası oluşturuldu. Oluşturulan bu dosyada textboxlar ve button lar yer almaktadır. Uygulama bir mesajlaşma uygulaması olduğu için connect, disconnect ve send butonları eklendi. ayrıca kullanıcıların mesajları yazabilmesi ve okuyabilmesi için gerekli textbox lar da form dosyasına eklendi.

3- Kodlama kısmında; form_load metodu içinde gerekli kodlar yazıldı. Bu kodlar genellikle hata kontrolü için veya kullanıcının yanlış bir şey yapmasını engellemek içindir. Connect butonu için; port içine gelen veriyi gönderebilmek için serialPort1 nesnesi projeye eklendi. Ayrıca bu bolümde Baud Rate, Parity, Stop Bits ve Data Bits gibi özelliklerin değerleri atandı. Bunlara ek olarak hata kontrolü için serialPort1 in bağlantı metodu try-catch içine yazılmıştır. Devamında ayni şekilde Disconnect butonu için gerekli kodlar yazıldı. Ve Sender butonu için gerekli kodlar yazıldı. 

4- Son bölümde, iki port arasında bir bağlantı testi için serialPort1_DataReceiver metodu eklendi. Bu sayede UI ve thread kısımları kontrol altına alındı. Çünkü uygulamanın amacı minimum iki thread ile çalışmaktır. DataReceiver'ın içine gerekli kodlar yazıldı.

5- Dip not: Kodun detaylı anlatımı yorum satırları ile belirtilmiştir.