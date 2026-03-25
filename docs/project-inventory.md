# Inventory پروژه‌ها و پکیج‌های Zamin

## Onion

### Zamin.Core.Domain
- **Path:** `Onion/src/2.Core/Zamin.Core.Domain/Zamin.Core.Domain.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Core.Domain`
- **Category:** `Onion`
- **Type:** `Core`
- **Purpose:** `اجزای پایه دامنه برای پیاده‌سازی DDD در لایه Domain.`
- **Main Dependencies:** ``
- **Used By:** `Core and application packages`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `هسته پروژه است و سعی شده به جز یوتیلیتی داخلی همین سلوشن وابستگی دیگری نداشته باشد`

### Zamin.Core.Domain.Toolkits
- **Path:** `Onion/src/2.Core/Zamin.Core.Domain.Toolkits/Zamin.Core.Domain.Toolkits.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Core.Domain.Toolkits`
- **Category:** `Onion`
- **Type:** `Utility`
- **Purpose:** `بعضی valueobjectها و entityها در همه پروژه ها استفاده میشوند. مثل نام، عنوان، کد ملی و ... که آن ها را در این پروژه قرارداده ایم`
- **Main Dependencies:** ``
- **Used By:** `samples `
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `وجود همچین پروژه ای شاید فریم ورک را پیجیده کند و احتمالا بتواند جایی خارج از فریم ورک به عنوان یک پکیج جداگانه مورد استفاده قرار گیرد. باید این موضوع دقیق فکر و بررسی شود.`

### Zamin.Core.Contracts
- **Path:** `Onion/src/2.Core/Zamin.Core.Contracts/Zamin.Core.Contracts.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Core.Contracts`
- **Category:** `Onion`
- **Type:** `Core`
- **Purpose:** `لایه اپلیکیشن سرویس Interfaceهای مورد نیاز خود را باید ارائه دهد. برای اینکه این Interfaceها کنار پیاده‌سازی ها قرار نگیرند این لایه ایجاد شده است. قراردادهایی مانند Interfaceهای ریپوزیتوری ساقراردادهای Command و QueryDispatcher`
- **Main Dependencies:** ``
- **Used By:** `ApplicationServices and Infrastructure `
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `شاید بهتر باشد مرج شود و باید به این موضوع فکر کرد و درباره آن گفتگو کرد. شاید با توجه به آمار بالای استفاده شرایط انتقال و مهاجرت را سخت کند`

### Zamin.Core.ApplicationServices
- **Path:** `Onion/src/2.Core/Zamin.Core.ApplicationServices/Zamin.Core.ApplicationServices.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Core.ApplicationServices`
- **Category:** `Onion`
- **Type:** `Core`
- **Purpose:** `در معماری کلان این لایه مربوط به نگهداری یوزکیس ها است. در فریم ورک هم زیرساخت های مورد نیاز این لایه مثل CommandDispatcher اینجا قرار گرفته است`
- **Main Dependencies:** ``
- **Used By:** `EndPoints and application implementations `
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** ``

### Zamin.Core.RequestResponse
- **Path:** `Onion/src/2.Core/Zamin.Core.RequestResponse/Zamin.Core.RequestResponse.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Core.RequestResponse`
- **Category:** `Onion`
- **Type:** `Core`
- **Purpose:** `در پروژه های اصلی این لایه پارامترهای ورودی و خروجی APIها را نگهداری میکند که در صورت نیاز با پروژه های بلیزور به اشتراک بگذارد. در این پروژه  اما فریم ورک و کلاسهای پایه این کار قرارگرفته مانند ICommandو CommandResult`
- **Main Dependencies:** ``
- **Used By:** `ApplicationServices and EndPoints `
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `شاید بهتر باشد این پروژه حذف شود یا با Contracts ادغام شود که برای این کار باید بررسی و گفتگوی کاملی انجام شود.`


### Zamin.Utilities
- **Path:** `Onion/src/1.Utilities/Zamin.Utilities/Zamin.Utilities.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Utilities`
- **Category:** `Onion`
- **Type:** `Utility`
- **Purpose:** `کلاسهای کاربردی مثل کار با تاریخ یا Enum یا Validation و ExtensionMethodهایی روی رشته ها در این پروژه قرارگرفته اند. تقریبا هرجایی میتوانند اسنفاده شوند`
- **Main Dependencies:** ``
- **Used By:** `Core and application packages`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `در ساختار معماری Onion وجود ندارد و صرفا در ابتدای کار اینجا قرارگرفته.شاید میتوانست در پروژه و سلوشن دیگری باشد یا کلا خارج از فریم ورک باشد و صرفا همراه آن استفاده شود. ولی در حال حاضر کار استفاده را ساده کرده است.`

### Zamin.Infra.Data.Sql
- **Path:** `Onion/src/3.Infra/Data/Zamin.Infra.Data.Sql/Zamin.Infra.Data.Sql.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Infra.Data.Sql`
- **Category:** `Onion`
- **Type:** `Infrastructure`
- **Purpose:** `در پروژه های واقعی در لایه Infra پیاده‌سازی ها انجام میشود. یکی از آنها کار با داده است. ما از CQRS استفاده میکنیم. چون هم در کامند هم کوئری کارهای مشتری برای ی دیتابیس وجود دارد بخش مشترک ددر این پروژه قرار گرفته است و زیرساختی و فریم ورکی است و در پروژه واقعی رفرنس داده ویشود`
- **Main Dependencies:** ``
- **Used By:** `Core and application packages`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** ``

### Zamin.Infra.Data.Sql.Commands
- **Path:** `Onion/src/3.Infra/Data/Zamin.Infra.Data.Sql.Commands/Zamin.Infra.Data.Sql.Commands.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Infra.Data.Sql.Commands`
- **Category:** `Onion`
- **Type:** `Infrastructure`
- **Purpose:** `کارهای زیرساختی و کلاسهای پایه Command مثل کلاس پایه برای DbContext مربوط به Commandاینجا تعریف میشود`
- **Main Dependencies:** ``
- **Used By:** `Core and application packages`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** ``

### Zamin.Infra.Data.Sql.Queries
- **Path:** `Onion/src/3.Infra/Data/Zamin.Infra.Data.Sql.Queries/Zamin.Infra.Data.Sql.Queries.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.Infra.Data.Sql.Queries`
- **Category:** `Onion`
- **Type:** `Infrastructure`
- **Purpose:** `کارهای زیرساختی و کلاسهای پایه Queries مثل کلاس پایه برای DbContext مربوط به Queries تعریف میشود`
- **Main Dependencies:** ``
- **Used By:** `Core and application packages`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** ``


### Zamin.EndPoints.Web
- **Path:** `Onion/src/4.EndPoints/Zamin.EndPoints.Web/Zamin.EndPoints.Web.csproj`
- **Solution:** `Zamin.sln`
- **Package Id:** `Zamin.EndPoints.Web`
- **Category:** `Onion`
- **Type:** `Endpoint`
- **Purpose:** `در پروژه های واقعی در این لایه API قرار میگیرد. اما اینجا کلاسهایی پایه ای که هر WebAPI به آن نیاز دارد قرار گرفته. مثلا تعدادی Middleware و ModelBindingو Filters و Conroller در ضمن چون تزریق وابستگی اینجا انجام میشود تعد ادی ExtensionMEthod جهت کانفیگ کل پروژه و فریم ورک اینجا قرار داد که در پروژه استافاده کننده باید صدا زده بشوند.`
- **Main Dependencies:** ``
- **Used By:** `Core and application packages`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** ``

## Extensions

### Abstractions Family

### Zamin.Extensions.Caching.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.Chaching.Abstractions/Zamin.Extensions.Caching.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.Caching.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `اینترفیس های پایه برای کار با کش که بعدا در پروژه های دیگری پیاده‌سازی میشود و در پروژه های اصلی به کمک تزریق وابستگی مورد استفاده نهایی قرار میگیرد`
- **Main Dependencies:** ``
- **Used By:** `Infrastructure and application packages needing cache services`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Folder structure seems inconsistent. Needs cleanup.به نظر میرسد باید در ابتدا ارتقا به 10 داشته باشد یک پیاده‌سازی فیک هم دارد که برای پروژه هایی است که نیازی به کش ندارند اما چون فریم ورک به صورت توکار نیاز به یک اینستنس از آن دارد نسخه فیک را قرارداده ایم تا اگر پیاده‌سازی لازم نبود از این نسخه استفاده شود.Critical abstraction package. Candidate for early .NET 10 migration.  Has a fake/no-op implementation used when no cache provider is configured.`


### Zamin.Extensions.ChangeDataLog.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.ChangeDataLog.Abstractions/Zamin.Extensions.ChangeDataLog.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.ChangeDataLog.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `نیاز داریم تغییرات Entityها را نگهداری کنیم که بدانیم چه کسی چه چیزی را تغییر داده است. کلاسها ی پایه برای این کار در این پروژه قرار دارد`
- **Main Dependencies:** ``
- **Used By:** `Infrastructure packages related to persistence and auditing`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration. Includes types such as DatabaseChangeType, EntityChangeInterceptorItem, IEntityChangeInterceptorItemRepository, and PropertyChangeLogItem.`

### Zamin.Extensions.DependencyInjection.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.DependencyInjection.Abstractions/Zamin.Extensions.DependencyInjection.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.DependencyInjection.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `به صورت پیش فرض سرویس هایی داریم که تزریق وابستگی خودکار دارند. اما بعضی سرویس ها که توسعه دهنده بدون استفاده از فریم ورک توسعه میدهد نیاز به تزریق دستی دارد. برای اینکه تزریق دستی انجام نشود سه اینترفیس برای مارکر داده ایم که برای سرویس های کاستوم توسعه دهنده استفاده کند و برای هر سرویسی قرار دهد با طول عمر متناسب با نام مارکر اینترفیس سرویس خودکار تزریق میشود`
- **Main Dependencies:** ``
- **Used By:** `Framework and consumer projects using automatic registration`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration.`

### Zamin.Extensions.Events.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.Events.Abstractions/Zamin.Extensions.Events.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.Events.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `قرارداد های outbox pattern است. یعنی یک کلاس برای Entity که در Outbox ذخیره میشود و تعریف IOutBoxEventItemRepository که بعدا باید پیاده‌سازی شود`
- **Main Dependencies:** ``
- **Used By:** `Infrastructure packages implementing outbox pattern`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration.`


### Zamin.Extensions.Logger.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.Logger.Abstractions/Zamin.Extensions.Logger.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.Logger.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `از آنجا که رهگیری لاگ ها با EventID ساده است قصد داشتم این کلاس محلی برای نگهداری EventIDهای فریم ورک زمین باشد. یک کلاس دارم ZaminEventId که داخل آن تعدادی EventId مثل PerformanceMeasurementو CommandValidationو ... قرارگرفته اند که نگهدارند یک مقدار عددی ثابت هستند`
- **Main Dependencies:** ``
- **Used By:** `Framework packages producing structured logs`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration. May not be a true abstraction package. Needs naming and responsibility review.`

### Zamin.Extensions.MessageBus.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.MessageBus.Abstractions/Zamin.Extensions.MessageBus.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.MessageBus.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `یک ساختار داده برای ابسترکت کردن کار با صف هایی مثل ربیت ام کیو اینجا قرار گرفته. کلاس پایه Parcel پیامی است که ارسال می شود. IReceiveMessageBus برای ت و ISendMessageBus برای ارسال پیام است که Publishو SendCommandToو Send دارد برای استفاده کننده ها هم اینترفیس IMessageConsumer قرار دارد که باید پیاده‌سازی کنندو.اینترفیس IMessageInboxItemRepository هم برای پیاده‌سازی اینباکس اینجا قرار دارد.`
- **Main Dependencies:** ``
- **Used By:** `Infrastructure packages implementing messaging and inbox/outbox patterns`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `با طراحی این ویژگی کلا مشکل دارم. حس میکنم نیاز به بازبینی دارد. باید این مورد را دقیق بررسی کنیم و این نیاز را طراحی صحیح کنیم و شاید چند قرارداد کم یا زیاد شود. چون پایه است باید در ابتدا به نسخه 10 مهاجرت کند. Critical abstraction package. Candidate for early .NET 10 migration. Critical abstraction package. Candidate for early .NET 10 migration. Messaging model needs architectural review.`


### Zamin.Extensions.ObjectMappers.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.ObjectMappers.Abstractions/Zamin.Extensions.ObjectMappers.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.ObjectMappers.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `مپ کردن اشیا با استفاده از Mapster یا اتو مپر کار مرسومی هست. برای اینکه از این ابزارها مستقیم استفاده نکنم اینجا یک IMapperAdapter قرار دادم تا بعدا با کتابخانه های مختلف پیاده‌سازی کنم و در پروژه ها تزریق و استفاده کنم`
- **Main Dependencies:** ``
- **Used By:** `Application and endpoint packages needing object mapping`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration.`

### Zamin.Extensions.Serializers.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.Serializers.Abstractions/Zamin.Extensions.Serializers.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.Serializers.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `سریالایز کردن به جیسان یا اکسل کار مرسومی هست که کتابخانه های مختلفی دارد و داخلی دات نت هم پیاده‌سازی دارد. برای اینکه این کار هارد کد نشود و یکدست باقی بماند IExcelSerializerوIJsonSerializer را اینجا قرار دادیم و بعدا پیاده‌سازی های مختلف آن را انجام میدهیم و در هر پروژه ای که نیاز بود تزریق میکنیم.`
- **Main Dependencies:** ``
- **Used By:** `Infrastructure and application packages needing serialization`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration.`

### Zamin.Extensions.Translations.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.Translations.Abstractions/Zamin.Extensions.Translations.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.Translations.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `با نوشتن متن وسط کد همیشه مشکل داشتم. برای اینکه کد کثیف نشه یه ITranslator درست کردم که هرجا متن لازم بود کلید یا کلیدهایی رو میگیره و موقع پیاده‌سازی که بعدا انجام میشه میره متن ها رو از دیتابیس میخونه متناسب با زبان برنامه و متن رو آماده میکنه و به خروجی میده. اینجا فقط اینترفیس هست و بعدا پیاده‌سازی انجام میشه. چ`
- **Main Dependencies:** ``
- **Used By:** `Application and endpoint packages needing text translation`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration. مشکلاتی با این پروژه دارم. توی پیاده‌سازی چون فرهنگ رو از برنامه میخونه برای زبان های مختلف باید Instancedهای مختلف از برنامه اجرا بشه که خوب نیست باید اصلاح بشه. در ضمن نحوه نوشتن و خوندن از دیتابیس که فقط SQL Server هم هست باید بازبینی بشه و بهره وری بالایی هم باید داشته باشه  Translation model, culture resolution, and storage strategy need redesign.`


### Zamin.Extensions.UsersManagement.Abstractions
- **Path:** `Extensions/Zamin.Extensions.Abstractions/Zamin.Extensions.UsersManagement.Abstractions/Zamin.Extensions.UsersManagement.Abstractions.csproj`
- **Solution:** `Zamin.Extensions.Abstractions`
- **Package Id:** `Zamin.Extensions.UsersManagement.Abstractions`
- **Category:** `Extensions`
- **Type:** `Abstraction`
- **Purpose:** `در لایه های مختلف به اطلاعاتی مانند نام کاربر و ... وابسته هستیم. برای اینکه به سرویس خاصی وابسته نشویم IUserInfoService را اینجا قرار دادیم که متدهایی جهت دریافت اطلاعات کاربر دارد و بعدا نسخه های مختلفی پیاده میکنیم مثلا نسخه ای داریم برای پیاده‌سازی خواندن از اچ تی تی پی و سرویس های مایکروسافت دات نت`
- **Main Dependencies:** ``
- **Used By:** `Application, infrastructure, and endpoint packages needing current user information`
- **Public Package:** `Yes`
- **Packable:** `Yes`
- **Current Target Framework:** `net9.0`
- **Notes:** `Critical abstraction package. Candidate for early .NET 10 migration.`


## Implementation Families

در بخش Extensions، پیاده‌سازی abstractionها در قالب familyهای زیر سازمان‌دهی شده‌اند:

- Caching
- Databases (ChangeDataLog)
- DependencyInjection
- Events
- MessageBus
- ObjectMappers
- Serializers
- Translations
- UsersManagement

## Common Structure

در اغلب این familyها، الگوی ساختاری یکسانی رعایت شده است:

- هر implementation در یک پوشه با نام کامل خود قرار دارد.
- هر implementation معمولاً شامل:
  - یک پروژه اصلی (Main)
  - یک پروژه Sample (در صورت نیاز)
  - یک پوشه `Extensions` شامل متدهای registration در `ServiceCollectionExtensions`
- ثبت سرویس‌ها از طریق Extension Method روی `IServiceCollection` انجام می‌شود.
- implementationها به abstractionهای متناظر در `Zamin.Extensions.*.Abstractions` وابسته هستند.

---

## Caching

### Zamin.Extensions.Caching.Distributed.Redis
- **Family:** `Caching`
- **Main Project:** `Zamin.Extensions.Caching.Distributed.Redis`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Caching.Abstractions, Redis, Zamin.Extensions.Serializers.Abstractions,Microsoft.Extensions.Caching.StackExchangeRedis`
- **Purpose:** `پیاده‌سازی caching مبتنی بر Redis.`
- **Notes:** `Distributed cache implementation.`

### Zamin.Extensions.Caching.Distributed.Sql
- **Family:** `Caching`
- **Main Project:** `Zamin.Extensions.Caching.Distributed.Sql`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Caching.Abstractions, SQL Server,Zamin.Extensions.Serializers.Abstractions, Microsoft.Extensions.Caching.SqlServer`
- **Purpose:** `پیاده‌سازی caching مبتنی بر دیتابیس.`
- **Notes:** `Potential performance considerations.`

### Zamin.Extensions.Caching.InMemory
- **Family:** `Caching`
- **Main Project:** `Zamin.Extensions.Caching.InMemory`
- **Sample Project:** `No`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Caching.Abstractions, Microsoft.Extensions.Caching.Memory, Zamin.Extensions.Serializers.Abstractions`
- **Purpose:** `پیاده‌سازی caching در حافظه.`
- **Notes:** `Simple and fast local cache.`

---

## Databases (ChangeDataLog)

### Zamin.Extensions.ChangeDataLog.Hamster
- **Family:** `Databases`
- **Main Project:** `Zamin.Extensions.ChangeDataLog.Hamster`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.ChangeDataLog.Abstractions`
- **Purpose:** `این پروژه رو داخلی پیاده کردیم و وابستگی خاصی نداره. اسمش رو خودم انتخاب کردم چون پروژه زمین هست زیر مجموعه ها رو اسم حیوانات میذارم`
- **Notes:** `Custom implementation. Needs evaluation.`

### Zamin.Extensions.ChangeDataLog.Sql
- **Family:** `Databases`
- **Main Project:** `Zamin.Extensions.ChangeDataLog.Sql`
- **Sample Project:** `No`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.ChangeDataLog.Abstractions, SQL Server`
- **Purpose:** `همستر برای ذخیره و بازیابی به دیتابیس نیاز داره. چون ممکنه بعدا بخایم از دیتابیس دیگه ای استفاده کنیم لایه دیتا اکسس رو جدا در این پروژه نگهداری میکنیم. برای ذخیره و بازیابی از Dapper 2.1.35 استفاده شده`
- **Notes:** `Primary persistence implementation. عملا به تنهایی کاری نمیکنه بلکه به خود پروژه همستر خدمت میده`

---

## DependencyInjection

### Zamin.Extensions.DependencyInjection
- **Family:** `DependencyInjection`
- **Main Project:** `Zamin.Extensions.DependencyInjection`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.DependencyInjection.Abstractions`
- **Purpose:** `پیاده‌سازی مکانیزم auto-registration برای سرویس‌ها.`
- **Notes:** `Core DI integration package. از اونجا که کلاس پایه ای برای پیاده‌سازی نیست نکته خاصی نداره. صرفا آپشن پترن هست که اسم اسمبلی که باید اسکن بشه رو دریافت میکنه`

---

## Events

### Zamin.Extensions.Events.Outbox
- **Family:** `Events`
- **Main Project:** `Zamin.Extensions.Events.Outbox`
- **Sample Project:** `No`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Events.Abstractions`
- **Purpose:** `پیاده‌سازی الگوی Outbox.`
- **Notes:** `Critical infrastructure for reliable messaging. یه Interceptor برای EF پیاده‌سازی شده و از کلاس BaseCommandDbContext هم ارث بری کردم به اسم BaseOutboxCommandDbContext که داخلش یه DbSet<OutBoxEventItem> OutBoxEventItems داره و AddOutBoxEventItemInterceptor Eventهای اگریگیت روت رو میخونه و تبدیل به انتیتی این لایه میکنه و موقع SaveChanges ذخیره میکنه`

### Zamin.Extensions.Events.PollingPublisher
- **Family:** `Events`
- **Main Project:** `Zamin.Extensions.Events.PollingPublisher`
- **Sample Project:** `No`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Events.Abstractions,Zamin.Extensions.MessageBus.Abstractions, Dapper`
- **Purpose:** `انتشار eventها با استفاده از polling.`
- **Notes:** `May have performance considerations. چون باید به دیتابیس وصل بشه لایه دیتابیس رو در Zamin.Extensions.Events.PollingPublisher.Dal.Dapper جدا کردم و همونطور که از اسمش معلومه از دپر برای واکشی استفاده میکنه. ایونت هایی که در outbox هست رو میخونه رو میزنه توی خروجی چون باید توی صف ثبت کنه از Zamin.Extensions.MessageBus.Abstractions هم استفاده میکنه برای ارسال. بخاطر پیچیدگی زیادی که داره نیاز به نمونه پروژه داره که نداره الان`

---

## MessageBus

### Zamin.Extensions.MessageBus.MessageInbox
- **Family:** `MessageBus`
- **Main Project:** `Zamin.Extensions.MessageBus.MessageInbox`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.MessageBus.Abstractions, Zamin.Extensions.Serializers.Abstractions, Dapper`
- **Purpose:** `پیاده‌سازی inbox pattern برای message handling.`
- **Notes:** `Complements outbox pattern. برای کار کردن با دیتابیس لایه Zamin.Extensions.MessageBus.MessageInbox.Dal.Dapper هم اضافه شده که داخلش با دپر به دیتابیس وصل میشم. پیاده‌سازی کاملی نداره و باید بازبینی بشه`

### Zamin.Extensions.MessageBus.RabbitMQ
- **Family:** `MessageBus`
- **Main Project:** `Zamin.Extensions.MessageBus.RabbitMQ`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.MessageBus.Abstractions, RabbitMQ, RabbitMQ.Client, Zamin.Extensions.Serializers.Abstractions, Zamin.Utilities`
- **Purpose:** `پیاده‌سازی message bus با RabbitMQ.`
- **Notes:** `External broker integration. پیاده‌سازی خیلی سخت و پیچیده است. باید بازبینی بشه بخصوص برای دریافت کننده و اینکه چجوری دریافت کننده وصل میشه به صف خیلی سخت هست و باید بازبینی دقیقی انجام بشه بعد از فاز تمیز کاری اولیه و آپدیت به نسخه 10`

---

## ObjectMappers

### Zamin.Extensions.ObjectMappers.AutoMapper
- **Family:** `ObjectMappers`
- **Main Project:** `Zamin.Extensions.ObjectMappers.AutoMapper`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.ObjectMappers.Abstractions, AutoMapper`
- **Purpose:** `Adapter برای AutoMapper.`
- **Notes:** `Widely used mapping implementation.`

### Zamin.Extensions.ObjectMappers.Mapster
- **Family:** `ObjectMappers`
- **Main Project:** `Zamin.Extensions.ObjectMappers.Mapster`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.ObjectMappers.Abstractions, Mapster`
- **Purpose:** `Adapter برای Mapster.`
- **Notes:** `Alternative mapping implementation.`

---

## Serializers

### Zamin.Extensions.Serializers.EPPlus
- **Family:** `Serializers`
- **Main Project:** `Zamin.Extensions.Serializers.EPPlus`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Serializers.Abstractions, EPPlus`
- **Purpose:** `پیاده‌سازی serializer برای Excel.`
- **Notes:** `Excel-specific serializer.`

### Zamin.Extensions.Serializers.Microsoft
- **Family:** `Serializers`
- **Main Project:** `Zamin.Extensions.Serializers.Microsoft`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Serializers.Abstractions, System.Text.Json`
- **Purpose:** `پیاده‌سازی JSON serializer با System.Text.Json.`
- **Notes:** `Default .NET serializer.`

### Zamin.Extensions.Serializers.NewtonSoft
- **Family:** `Serializers`
- **Main Project:** `Zamin.Extensions.Serializers.NewtonSoft`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Serializers.Abstractions, Newtonsoft.Json`
- **Purpose:** `پیاده‌سازی JSON serializer با Newtonsoft.`
- **Notes:** `Legacy/advanced serializer support.`

---

## Translations

### Zamin.Extensions.Translations.Parrot
- **Family:** `Translations`
- **Main Project:** `Zamin.Extensions.Translations.Parrot`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.Translations.Abstractions`
- **Purpose:** `پپیاده‌سازی ذخیره و بازیابی ترجمه ها و متن ها با استفاده از کلید در دیتابیس که برای علاقه به پرنده ها اسمش شد پرت چون اگر کلید رو پیدا نکنه توی دیتابیس همون متن کلید رو برمیگردونه`
- **Notes:** `به دیتابیس پیاده‌سازی وابسته است. باید جدا بشه لایه دیتا اکسس که اگر دیتابیس تغییر کرد تغییر اون امکان پذیر باشه. کلا همه دیتابیس ها یه نسخه PostgreSQL هم باید براشون انجام بشه حداقل حتی بقیه پروژه های بالایی که داخلشون گفته نشده مثل outbox و Inbox`

---

## UsersManagement

### Zamin.Extensions.UsersManagement
- **Family:** `UsersManagement`
- **Main Project:** `Zamin.Extensions.UsersManagement`
- **Sample Project:** `Yes`
- **Registration Extension:** `Yes`
- **Depends On:** `Zamin.Extensions.UsersManagement.Abstractions`
- **Purpose:** `پیاده‌سازی دسترسی به اطلاعات کاربر جاری. این اطلاعات از HttpContext میاد. `
- **Notes:** `دو تا پروژه سمپل داره یکی برای MVC و یکی برای WebAPI`


## Root Utilities

### Zamin.Utilities.OpenTelemetryRegistration
- **Main Project:** `Zamin.Utilities.OpenTelemetryRegistration`
- **Type:** `Registration`
- **Purpose:** `ثبت OpenTelemetry برای پروژه‌های مصرف‌کننده`
- **Depends On:** `OpenTelemetry, ASP.NET Core`
- **Used By:** `Web/API projects`
- **Notes:** `این پروژه برای ساده‌سازی ثبت OpenTelemetry ساخته شده است. دارای پروژه Sample است و یک middleware برای ثبت ورودی و خروجی درخواست‌ها نیز دارد.`

### Zamin.Utilities.Auth.ApiAuthentication
- **Main Project:** `Zamin.Utilities.Auth.ApiAuthentication`
- **Type:** `Auth`
- **Purpose:** `برای ثبت و مدیریت Authentication در APIها استفاده میشه`
- **Depends On:** `Microsoft.AspNetCore.Authentication.JwtBearer, IdentityModel, IdentityModel.AspNetCore.OAuth2Introspection, ASP.NET Core`
- **Used By:** `Web/API projects`
- **Notes:** `این پروژه قرار بوده فرایند فعال‌سازی لاگین را ساده کند، اما در حال حاضر شامل کلاس‌های پایه و اجزای متعددی برای Auth است. این بخش خارج از جریان اصلی توسعه شکل گرفته و نیازمند بازبینی جدی معماری و مسئولیت‌هاست. هدف آن ساده‌سازی فعال‌سازی OpenIdConnect/OAuth2 روی پروژه‌هاست. احتمالاً برای MVC نیز به نسخه یا مسیر جداگانه نیاز است.`


### Zamin.Utilities.ScalarRegistration
- **Main Project:** `Zamin.Utilities.ScalarRegistration`
- **Type:** `Registration`
- **Purpose:** `برای ثبت Scalar به عنوان UI برای Open API`
- **Depends On:** `Microsoft.AspNetCore.OpenApi, Scalar.AspNetCore`
- **Used By:** `Web/API projects`
- **Notes:** `Registration-only package for Scalar UI integration.`

### Zamin.Utilities.SerilogRegistration
- **Main Project:** `Zamin.Utilities.SerilogRegistration`
- **Type:** `Logging`
- **Purpose:** `برای ثبت ساده Serilog که در فولدر SerilogRegistration قرار گرفته و تعدادی هم Enricher اختصاصی داخلش قرار گرفته`
- **Depends On:** `Serilog`
- **Used By:** `Web/API projects`
- **Notes:** `علاوه بر registration ساده Serilog، شامل enrichers اختصاصی نیز هست؛ بنابراین صرفاً یک registration package ساده نیست و باید به‌عنوان بخشی از زیرساخت logging دیده شود.`

### Zamin.Utilities.Sinks.Elasticsearch
- **Main Project:** `Zamin.Utilities.Sinks.Elasticsearch`
- **Type:** `Logging`
- **Purpose:** `داخل فولدر Sinks و بعد از اون Elasticsearch قرار گرفته و یه رجیستر خاص برای الستیک سرچ هست`
- **Depends On:** `Serilog`
- **Used By:** `Web/API projects`
- **Notes:** `پیاده‌سازی sink اختصاصی برای Elasticsearch. با توجه به وجود SerilogRegistration، باید بررسی شود که آیا این پروژه باید جدا بماند یا در ساختار logging ادغام شود.`

### Zamin.Utilities.SoftwarepartDetector
- **Main Project:** `Zamin.Utilities.SoftwarepartDetector`
- **Type:** `Capability`
- **Purpose:** `داخل فولدر SoftwarepartDetector قرار گرفته. توی استارت آپ برنامه میاد تمامی اکشن متدها رو کشف میکنه بعد تشخیص میده داخل کدوم کنترلر هستن. بعد از تنظیمات برنامه میخونه اسم برنامه چیه و اگر برنامه میکروسرویس از یک سیستم بزگتر باشه اسم سامانه اصلی چیه و در نهایت یک درخت چهار سطحی از این داده ها میسازی یعنی بخش ها یا یک اکشن هستن یا یک کنترلر یا یک میکروسرویس یا بخشی از یک ماژول بزگرتر یا بخشی از یک سامانه کامل. این داده ها رو که دست آورد ارسال میکنه برای یه وب ای پی آی که باید اون باید اطلاعات ارسالی رو توی دبتایس خودش ذخیره کنه. بعدا این درخت ها میتونن موقع تعیین سطح دسترسی به کاربران نشون داده بشن یا میشه از روی اون ها منو جنریت کردو.و ولی اینجا صرفا یک داده خام درختی هست که ایجاد شده`
- **Depends On:** ``
- **Used By:** `Web/API projects`
- **Notes:** `قابلیتی مستقل برای کشف ساختار نرم‌افزار و تولید درخت اجزای قابل استفاده در سناریوهایی مانند مدیریت دسترسی و تولید منو. این پروژه صرفاً registration نیست و باید به‌عنوان یک capability مستقل دیده شود.`