# Proyecto de Backend - SistemaInventarios

Este proyecto es una API RESTful para Organizar eventos. El backend está construido con .Net 8.0 y utiliza una base de datos Sql Server.
La arquitectura esta implementada en arquitectura hexagona con CQRS utilizando el patron mediator.


# Base de datos
El Nombre de la base de datos es EventManagemetDB

# Migracion de base de datos
Para poder realizar la migracion de la base de datos esta diseñada mediante Code-First en el contexto de Entity Framework Core (EF Core) 
puedes utilizar estos comandos. en la consola de paquetes de .net o con power-shell
- add-Migration initial
- Update-database
---

## Requisitos previos

Antes de comenzar, asegúrate de tener instalados los siguientes paquetes en tu sistema:

- [AutoMapper.Extensions.Microsoft.DependencyInjection] (v12.0.0)
- [MediatR.Extensions.Microsoft.DependencyInjection] (v10.0.1)
- [Microsoft.EntityFrameworkCore.Design] (v8.0.12)
- [Microsoft.EntityFrameworkCore.SqlServer] (v8.0.12)
- [Microsoft.EntityFrameworkCore.Tools] (v8.0.12)

# Proyecto de Front-end - HotelReservation

Este proyecto está construido utilizando Angular v18.0.6, un framework moderno y eficiente para aplicaciones web. Proporciona una base sólida para desarrollar interfaces de usuario rápidas y reactivas.

---

## Requisitos previos
Asegúrate de tener instalado lo siguiente:

Node.js: Versión v20.15.0 o inferior conpatible con la version v18.0.6 de angular. Descargar Node.js
npm: Administrador de paquetes que se instala automáticamente con Node.js.

## Servidor 
El servidor estará disponible en http://localhost:5029.

## Acceso
Acceso a la APP: http://localhost:4200/login

Acceso login

-Puede crear los usuarios a traves del boton de registrarse

## Configuración inicial

1. **Clonar el repositorio**

   Clona este repositorio en tu máquina local utilizando el siguiente comando:

   https://github.com/jeissonLL/PruebaInterRapidisimo.git
   