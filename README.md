Merhabalar,

Proje tam anlamıyla istediğim seviyeye ulaşmamıştır. Zaman buldukça geliştirmeye devam edeceğim.

projeler çalıştırılmadan önce database migration işlemleri yapılmalıdır. Migartion yapılırken startup project olarak webapi projesi default projects kısmında ise persistence projeleri seçilmelidir.

Proje çalıştırma sırası:
1)Contact.WebAPI
2)Report.WebAPI
3)Contacts.APIGateway
4)WorkerProcess.ReportService

contact api üzerinden contact crud işlemleri contact info crud işlemleri ve info type crud işlemleri yapılmaktadır.
contact report üzerinden ise report log ve report log status crud işlemleri yapılmaktadır.

report log create olurken kuyruğa iş ekler.
bu işin yapılması için workerprocess.reportservice projesinin çalıştırılması gerekmektedir.

proje için testler tamamlanmamıştır. şuan unit testlerin bir kısmı vardır. ilerleyen süreçte tamamlanıp entegarasyon testleride yazılacaktır.
