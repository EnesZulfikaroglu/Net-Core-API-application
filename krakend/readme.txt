http://localhost:8080/__health endpointini kullanarak bağlantı test edilebilir.

krakend  -> tüm endpointlerin bir arada bulunduğu json dosyası, bu dosya üzerinden hepsi test edilebilir. 

krakend1 -> endpoint ismi değiştirme

krakend2 -> public bir apiden veri çekme ve 2 farklı api'den gelen verileri birleştirme kullanılır. Mapping örneği vardır.

krakend3 -> parametre gönderme

krakend4 -> no-op ayarı: request body, hiç değişiklik yapılmadan doğrudan gönderilir.

krakend5 -> Sequential proxy: zincirleme olarak request atar. İlk request ile gelen datayı kullanabilir.

krakend6 -> Static Proxy: Bazı durumlara (error, success, incomplete vs.) özel olarak 
static response'lar kullanılmasını sağlar.

krakend7 -> request ve response'ların kontrol edilerek koşullara uygun olanların kabul edilmesini sağlar.

krakend8 -> kullanılan JSON yapısında configuration örneği.

krakend9 -> Stateless yapıyı gösteren endpoint. Bir node çökse bile diğerleri bundan bağımsız olarak davranır.

krakend10 -> Response için filtreleme yapmayı sağlayan endpointler.
allow, izin verilen verileri, deny ise engellenen verileri belirler. Grouping kullanımı gösterilmiştir.





