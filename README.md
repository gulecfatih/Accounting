# Accounting

1  - Uygulamayı Başlatmadan Yapılacaklar  React.js Projesinde Api alan değerler başka bir bilgisayarda açılınca değişecektir. Bundan dolayı React.js Projesinde Apilerin ismi değiştirilmeli
2  - Roller için bir Ekran Oluşturulmadı Kullanıcı Kaydı Yapılmadan Önce .Net Core Api Projesi Açılıp Roller Tanımlanmalıdır
   - /api/Auth/RoleAdd Api ile 3 tane Role Tanımlanmalı Roller : "Salesperson","Manager","Accountant"

   - Bu gereksinimler Yerine getirdikten Sonra Artık Proje Açılabilir


  - Yapılanlar -
 Login Ekranı Gerçekleştirildi.
 Salesperson kullanıcısı için masraf ekleme ekranı ve eklenmiş masrafların listelenmesi sağlandı. (jwt token oluşturulması sağlandı cache ile bilgileri alamadığım için react.js cache mekanizmasıda kullanıldı.)
 Manager kullanıcısı için masrafları onaylayabilecek bir ekran oluşturuldu ekranda onaylanan masraflar ve onaylanacak Masraflar olarak 2 kısım bulunuyor
 Accountant kullanıcısı için masrafların ödendiği tikini atabileceği bir ekran oluşturuldu ödenecek ve ödenen masrafların 2 sini de gösterilmesi sağlandı
