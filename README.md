# SS Framework

![build and test](https://img.shields.io/github/actions/workflow/status/ssframework/ss/build-and-test.yml?branch=dev&style=flat-square)
[![codecov](https://codecov.io/gh/ssframework/ss/branch/dev/graph/badge.svg?token=jUKLCxa6HF)](https://codecov.io/gh/ssframework/ss)
[![NuGet](https://img.shields.io/nuget/v/SmartSoftware.Core.svg?style=flat-square)](https://www.nuget.org/packages/SmartSoftware.Core)
[![NuGet (with prereleases)](https://img.shields.io/nuget/vpre/SmartSoftware.Core.svg?style=flat-square)](https://www.nuget.org/packages/SmartSoftware.Core)
[![MyGet (nightly builds)](https://img.shields.io/myget/ss-nightly/vpre/SmartSoftware.svg?style=flat-square)](https://docs.smartsoftware.io/en/ss/latest/Nightly-Builds)
[![NuGet Download](https://img.shields.io/nuget/dt/SmartSoftware.Core.svg?style=flat-square)](https://www.nuget.org/packages/SmartSoftware.Core)
[![Code of Conduct](https://img.shields.io/badge/Contributor%20Covenant-v2.0%20adopted-ff69b4.svg)](https://github.com/ssframework/ss/blob/dev/CODE_OF_CONDUCT.md)
[![CLA Signed](https://cla-assistant.io/readme/badge/ssframework/ss)](https://cla-assistant.io/ssframework/ss)
[![SS Discord server](https://img.shields.io/discord/951497912645476422?label=Discord)](https://discord.gg/ss)

SS Framework is a complete **infrastructure** based on **ASP.NET Core** to create **modern web applications** and **APIs** by following the software development **best practices** and the **latest technologies**. Check out https://smartsoftware.io

## Getting Started

- [Quick Start](https://docs.smartsoftware.io/en/ss/latest/Tutorials/Todo/Index) is a single-part, quick-start tutorial to build a simple application with the SS Framework. Start with this tutorial if you want to quickly understand how SS works.
- [Getting Started guide](https://docs.smartsoftware.io/en/ss/latest/Getting-Started) can be used to create and run SS based solutions with different options and details.
- [Web Application Development Tutorial](https://docs.smartsoftware.io/en/ss/latest/Tutorials/Part-1) is a complete tutorial to develop a full stack web application with all aspects of a real-life solution.

### Quick Start

Install the SS CLI:

````bash
> dotnet tool install -g SmartSoftware.Cli
````

Create a new solution:

````bash
> ss new BookStore -u mvc -d ef
````

> See the [CLI documentation](https://docs.smartsoftware.io/en/ss/latest/CLI) for all available options.

### UI Framework Options

<img width="500" src="docs/en/images/ui-options.png">

### Database Provider Options

<img width="500" src="docs/en/images/db-options.png">

## The Book: Mastering SS Framework

Written by the creator of SS Framework, this book will help you to gain a complete understanding of the SS Framework and modern web application development techniques.

* [Buy on Amazon](https://www.amazon.com/gp/product/B097Z2DM8Q)
* [Buy on Packt Publishing](https://www.packtpub.com/product/mastering-ss-framework/9781801079242)
* [More details about the book](https://smartsoftware.io/books/mastering-ss-framework)

![book-mastering-ss-framework](docs/en/images/book-mastering-ss-framework.png)

## What SS Provides?

SS provides a **full stack developer experience**.

### Architecture

<img src="docs/en/images/ddd-microservice-simple.png">

SS offers a complete, **modular** and **layered** software architecture based on **[Domain Driven Design](https://docs.smartsoftware.io/en/ss/latest/Domain-Driven-Design)** principles and patterns. It also provides the necessary infrastructure and guidance to [implement this architecture](https://docs.smartsoftware.io/en/ss/latest/Domain-Driven-Design-Implementation-Guide).

SS Framework is suitable for **[microservice solutions](https://docs.smartsoftware.io/en/ss/latest/Microservice-Architecture)** as well as monolithic applications.

### Infrastructure

There are a lot of features provided by the SS Framework to achieve real world scenarios easier, like [Event Bus](https://docs.smartsoftware.io/en/ss/latest/Event-Bus), [Background Job System](https://docs.smartsoftware.io/en/ss/latest/Background-Jobs), [Audit Logging](https://docs.smartsoftware.io/en/ss/latest/Audit-Logging), [BLOB Storing](https://docs.smartsoftware.io/en/ss/latest/Blob-Storing), [Data Seeding](https://docs.smartsoftware.io/en/ss/latest/Data-Seeding), [Data Filtering](https://docs.smartsoftware.io/en/ss/latest/Data-Filtering), etc.

### Cross Cutting Concerns

SS also simplifies (and even automates wherever possible) cross cutting concerns and common non-functional requirements like [Exception Handling](https://docs.smartsoftware.io/en/ss/latest/Exception-Handling), [Validation](https://docs.smartsoftware.io/en/ss/latest/Validation), [Authorization](https://docs.smartsoftware.io/en/ss/latest/Authorization), [Localization](https://docs.smartsoftware.io/en/ss/latest/Localization), [Caching](https://docs.smartsoftware.io/en/ss/latest/Caching), [Dependency Injection](https://docs.smartsoftware.io/en/ss/latest/Dependency-Injection), [Setting Management](https://docs.smartsoftware.io/en/ss/latest/Settings), etc.

### Application Modules

SS is a modular framework and the Application Modules provide **pre-built application functionalities**;

- [**Account**](https://docs.smartsoftware.io/en/ss/latest/Modules/Account): Provides UI for the account management and allows user to login/register to the application.
- **[Identity](https://docs.smartsoftware.io/en/ss/latest/Modules/Identity)**: Manages organization units, roles, users and their permissions, based on the Microsoft Identity library.
- [**OpenIddict**](https://docs.smartsoftware.io/en/ss/latest/Modules/OpenIddict): Integrates to OpenIddict.
- [**Tenant Management**](https://docs.smartsoftware.io/en/ss/latest/Modules/Tenant-Management): Manages tenants for a [multi-tenant](https://docs.smartsoftware.io/en/ss/latest/Multi-Tenancy) (SaaS) application.

See the [Application Modules](https://docs.smartsoftware.io/en/ss/latest/Modules/Index) document for all pre-built modules.

### Startup Templates

The [Startup templates](https://docs.smartsoftware.io/en/ss/latest/Startup-Templates/Index) are pre-built Visual Studio solution templates. You can create your own solution based on these templates to **immediately start your development**.

## SS Community

### SS Community Web Site

The [SS Community](https://community.smartsoftware.io/) is a website to publish **articles** and share **knowledge** about the SS Framework. You can also create content for the community!

### Blog

Follow the [SS Blog](https://blog.smartsoftware.io/) to learn the latest happenings in the SS Framework.

### Samples

See the [sample projects](https://docs.smartsoftware.io/en/ss/latest/Samples/Index) built with the SS Framework.

### Want to Contribute?

SS is a community-driven open source project. See [the contribution guide](https://docs.smartsoftware.io/en/ss/latest/Contribution/Index) if you want to be a part of this project.

## Official Links

* <a href="https://smartsoftware.io/" target="_blank">Main Web Site</a>
  * <a href="https://smartsoftware.io/get-started" target="_blank">Get Started</a>
  * <a href="https://smartsoftware.io/features" target="_blank">Features</a>
* <a href="https://docs.smartsoftware.io/" target="_blank">Documentation</a>
* <a href="https://docs.smartsoftware.io/en/ss/latest/Samples/Index" target="_blank">Samples</a>
* <a href="https://blog.smartsoftware.io/" target="_blank">Blog</a>
* <a href="https://community.smartsoftware.io/" target="_blank">Community</a>
* <a href="https://stackoverflow.com/questions/tagged/ss" target="_blank">Stack overflow</a>
* <a href="https://twitter.com/ssframework" target="_blank">Twitter</a>

## Support the SS Framework

Love SS Framework? **Please give a star** to this repository :star:

## Discord Channel

You can use this link to join the SS Community Discord Server: https://discord.gg/ss

## SS Commercial

See also [SS Commercial](https://commercial.smartsoftware.io/) if you are looking for pre-built application modules, professional themes, code generation tooling and premium support for the SS Framework.
