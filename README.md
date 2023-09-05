# Facturas proyecto 
Este proyecto consiste en avisarle por correo electronico al cliente como se encuentra el estado de su factura, el cliente podra redireccionarne desde un link que se le compartira en el body del correo a consultar la informacion de su factura.
- **Backend:** Desarrollado en .NET.
- **Base de Datos:** Utilizamos MongoDB como sistema de gestión de bases de datos.
- **Frontend:** La interfaz de usuario se construyó utilizando el framework Angular.

## Configuracion del proyecto
Para la configuracion del proyecto, sigue estos pasos: 

### 1. Instalar MongoDB
si aun no tienes MongoDB instalado en tu ordenador, puedes descargar la version comunity Server desde el [https://www.mongodb.com/try/download/community]

### 2. Clonar el Repositorio 
Clona este repositorio en tu maquina local utilizando el siguiente comando: 

git clone https://tu-repositorio.git

### Configuracion de la base de datos
cuando ya se este en el servidor por defecto de mongoDB, lo puede hacer desde la linea de comandos o desde su gestor de base de datos de preferencia.
Se creare una base de datos llamada "prubea" con el siguiente comando 

use prueba 

depues de crear la database se crea la coleccion "Facturas"

db.createCollection('Facturas')

### Importacion de datos 
los datos de prueba se encuentran ubicados en el archivo Data.json, su gestor de la base de datos le permitira importar esos datos directamente del archivo,
para probar la funcionalidad de mandar correos, actualiza el parametro correoElectronico del objeto cliente por el correo al cual le gustaria recibir el correo de verifiacion. nota: para que se vea la funcionalidad del link que se encuentra en el correo, debe estar en el mismo ordenador que el proyecto del front corriendo.


### Como se usa?
Cuando se inicie el proyecto angular se mostrara una lista de facturas, se mostrara el icono de una lupa para el ver mas informacion de la factura, cuando se precione dicho icono lo mandara a ver la informacion de la factura y tendra dos opciones con dicha informacion podra enviar el recordatorio al cliente asociado a esa factura y el volver al lista, en dicho caso se mandara el recordatorio se mostraria un mensaje de confirmacion. Para ver la informacion de la factura siendo cliente se tendra que dar click al link que se encuentra en el correo del recordatorio. 










