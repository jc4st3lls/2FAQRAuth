# 2FAQRAuth
Autenticació de doble factor amb codi QR.
# La prova de concepte (POC)
El repte és com dir-li a una sessió d’un portal oberta en un navegador (estació client) que autentifiqui un usuari en el servidor, des de una App remota.
# Descripció
En la solució, s’implementa un portal Asp.Net Core amb autenticació activada, seguint l’esquema o patró per defecte d’identitats, autentificació i autorització de la pròpia tecnologia. 
Per defecte aquestes solucions aporten una part de autenticació (login), on l’usuari s’identifica amb un identificador i una contrasenya. En el portal presentat, substituïnt aquesta part per un codi QR, molt similar al sistema que fa servir el WhatsAppWeb. Aquests utilitza una opció de l’App per escanejar un codi presentat en la plana de navegador i transmetre les credencials (la identitat) de l’usuari de la App. En el nostre portal, també necessitarem d’una App per dur a terme l’Autenticació. En la solució s’adjunta una App (Android i iOS) per poder fer executar el procés.
# Funcionament
Primer de tot cal executar el portal. Es pot desplegar en local o en un servidor o en un contenidor docker. Un cop desplegat, hem de tenir clar en quin endpoint respon, per exemple **http://192.168.1.46:5000 (o https, 5001)**. 
Anem a la opció de login, i s’ens presenta un codi QR.
Engeguem la App. Demanarà usuari i password (Només existeix un usuari, amb una contrasenya, s’ha de cercar en el codi). Si entrem bé les credencials, s’obrira un scanner de codi QR. Escanejem la web, i si tot va bé, apareixerà un punt de menú nou en color blau que representa la part privada), el nom d’usuari i la opció de logout.
# A tenir en compte.
El portal fa servir l’esquema d’autenticació basat en cookies. Aquesta té un temps de vida, per tant si volem desconnectar la sessió millor prémer logout.
```
services.AddAuthorization()
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
```

En la App, cal configurar bé on es troba el portal. Això ho trobem en el constructor de la classe **App.cs**.
```
        public App()
        {
            InitializeComponent();

            PORTAL_SCHEMA = "http";
            PORTAL_HOST = "192.168.1.46";
            PORTAL_PORT = "5000";

            //PORTAL_SCHEMA = "https";
            //PORTAL_HOST = "192.168.1.46";
            //PORTAL_PORT = "5001";

            MainPage = new NavigationPage(new MainPage());
        }
```





