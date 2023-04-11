# ObligatorioRedes

## Descripción del Sistema

Se debe construir un sistema que cuenta con dos aplicaciones, un servidor en el que se debe manejar la información relacionada a monoplazas (autos de carrera), repuestos y categorías relacionados con los mismos. Además, contaremos con una aplicación cliente para dicho servidor que se encargará de la
interacción de los mecánicos con el sistema.

La plataforma debe brindar las siguientes funcionalidades:
1. Conexión de un cliente al servidor (previa autenticación).
2. Dar de alta a una solicitud de repuesto.
3. Crear Categorías de repuesto.
4. Asociar foto al repuesto.
5. Consultar repuestos existentes.
6. Enviar y recibir mensajes entre mecánicos.

**El sistema consta de dos módulos de software, servidor y cliente**

## Requerimientos Funcionales
**Aplicación Servidor.** 
- SRF0. Aceptar pedidos de conexión de un usuario. El servidor debe ser capaz de aceptar
pedidos de conexión de varios clientes a la vez.
- SRF1. Dar de Alta a un usuario. El sistema debe permitir dar de alta a un usuario (mecánico)
en el sistema. Esta funcionalidad solo puede ser realizada por el admin.
- SRF2. Dar de Alta a un repuesto. El sistema debe permitir dar de alta a un repuesto,
incluyendo id, nombre, proveedor y marca.
- SRF3. Crear Categoría de repuesto. El sistema debe permitir crear una categoría para los
repuestos.
- SRF4. Asociar Categorías a un repuesto. El sistema debe permitir asociar categorías a los
repuestos.
- SRF5 Asociar una foto al repuesto. El sistema debe permitir subir una foto y asociarla a un
repuesto específico.
- SRF6. Consultar repuestos existentes. El sistema deberá poder buscar repuestos existentes,
incluyendo búsquedas por palabras claves.
- SRF7. Consultar un repuesto específico. El sistema deberá poder buscar un repuesto
específico. También deberá ser capaz de descargar la imagen asociada, en caso de existir la
misma.
- SRF8. Enviar y recibir mensajes entre mecánicos. El sistema debe permitir que un mecánico
envíe mensajes a otro, y que el mecánico receptor chequee sus mensajes sin leer, así como
también revisar su historial de mensajes.
- SRF9. Configuración. Se deberá ser capaz de modificar los puertos e ip utilizados por el
servidor y la clave del usuario admin sin necesidad de recompilar el proyecto. Dichos valores
no deben estar “hardcodeados” en el código.

**Aplicación Cliente.**
- CRF0. Conectarse (previa autenticación) y desconectarse al servidor. Se deberá ser capaz de
conectarse y desconectarse del servidor, implica autenticación.
- CRF1. Alta de usuario. Se debe poder dar de alta a un usuario (mecánico). Esta
funcionalidad solo puede realizarse desde el usuario admin.
- CRF2. Alta de repuesto. Se debe poder dar de alta a un repuesto en el sistema, incluyendo
id, nombre, proveedor y marca.
- CRF3. Alta de Categoría de repuesto. El sistema debe permitir crear una Categoría para los
repuestos.
- CRF4. Asociar Categorías a los repuestos. El sistema debe permitir asociar categorías a los
repuestos.
- CRF5. Asociar foto a repuesto. El sistema debe permitir subir una foto y asociarla a un
repuesto específico.
- CRF6. Consultar repuestos existentes. El sistema deberá poder buscar repuestos existentes,
incluyendo búsquedas por palabras claves.
- CRF7. Consultar un repuesto específico. El sistema deberá poder buscar un repuesto
específico. También deberá ser capaz de descargar la imagen asociada, en caso de existir la
misma.
- CRF8. Enviar y recibir mensajes. El sistema debe permitir que un mecánico envíe mensajes
a otro, y que el mecánico receptor chequee sus mensajes sin leer, así como también revisar
su historial de mensajes.
- CRF9. Configuración. Se deberá ser capaz de modificar los puertos e ip utilizados por el
cliente y la clave del usuario admin sin necesidad de recompilar el proyecto. Dichos valores
no deben estar “hardcodeados” en el código.

**Especificación de protocolo**
1. El siguiente protocolo es una sugerencia, los estudiantes pueden (y deberían) hacer cambios al
mismo.
2. Protocolo orientado a caracteres.
3. Implementado sobre TCP/IP.
4. Los valores deberán ir alineados a la derecha, los bytes de relleno deberán tener el valor 0.
5. Los campos HEADER, CMD y LARGO tendrán largo fijo. El campo DATOS tendrá largo variable, según
el valor indicado en LARGO.
6. Formato general de la trama.
<img width="497" alt="Captura de pantalla 2023-04-11 a la(s) 17 33 04" src="https://user-images.githubusercontent.com/80791547/231281751-8b4eae98-ada2-4051-b377-47f12bca9f95.png">
