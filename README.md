
# DPWDR

## Descripcion ✍️ 

Un aplicativo, para mantener productos con procesos backgrounds

## Tabla de Contenidos 👈 

-[Instalación](#installation)
-[Versiones](#versiones)

## Instalacion   🔧 

•	Model Module

  Este modulo es un proyecto tipo class library, que contiene las definiciones del entity framwework ademas de los diferents Dtos y Entities, es la capa de acceso a datos.

•	Service Module

  Este modulo es un proyecto tipo class library, que contiene todo el core del aplicativo desde operaciones CRUD hasta procesos backgrounds.

•	Api Module

  Este modulo es un proyecto NET CORE Api, que sirve como API Gateway entre nuestro cliente final y nuestro servicio.

•	Client Module

  Este modulo es un aplicativo NET MVC, que contine las vistas y controladores necesarios para manejar el flujo interactivo del usuario.

•	Tester Module

  En este modulo utilizamos la arquitectura NUnit, para asegurar la integridad de nuestro codigo y la funcionalidad del aplicativo.

Diagrama del Flujo operativo del APP ✅

https://lucid.app/lucidspark/1621f831-d819-4a9b-a31e-6e79e4b4cb31/edit?invitationId=inv_4ae4320d-552b-4d66-ad97-da52b1eb3053&page=0_0#

Versions ✅

NET:
  - "Hangfire": "1.8.14",
  - "EntityFrameworkCore": "5.0.10",
  - "EntityFrameworkCore.SqlServer": "5.0.10",
  - "EntityFrameworkCore.Tools": "5.0.10",
  - "Microsoft.Extensions.Hosting.Abstractions": "8.0.0",
  - "Microsoft.Extensions.Http": "8.0.0",
    




