# مهاجرت از نسخه 9 به 10

## هدف

هدف این سند ثبت روند مهاجرت Zamin از .NET 9 به .NET 10، تصمیم‌های مهم، و ترتیب اجرای migration است.

## تغییرات زیرساخت build

در این مرحله، زیرساخت build و packaging به‌صورت متمرکز بازبینی و تنظیم شد:

- اضافه شدن `global.json` برای کنترل نسخه SDK
- اضافه شدن `Directory.Build.props` در ریشه repository
- اضافه شدن `Directory.Packages.props` در ریشه repository
- تعریف `Directory.Build.props` محلی برای solution مربوط به abstractionها
- فعال‌سازی تولید package هنگام build برای موج اول

## موج اول

موج اول شامل packageهای abstraction پایدار و پایه framework است:

- `Zamin.Extensions.Caching.Abstractions`
- `Zamin.Extensions.DependencyInjection.Abstractions`
- `Zamin.Extensions.Events.Abstractions`
- `Zamin.Extensions.ChangeDataLog.Abstractions`
- `Zamin.Extensions.ObjectMappers.Abstractions`
- `Zamin.Extensions.Serializers.Abstractions`
- `Zamin.Extensions.UsersManagement.Abstractions`
- `Zamin.Extensions.MessageBus.Abstractions`
- `Zamin.Extensions.Translations.Abstractions`

این packageها به این دلیل در موج اول قرار گرفتند که قراردادهای نسبتاً پایدار و نقش زیرساختی دارند و پایه implementationهای بعدی محسوب می‌شوند.

## خارج از موج اول

package زیر فعلاً از موج اول خارج شده است:

- `Zamin.Extensions.Logger.Abstractions`

### دلیل
مسئولیت این پروژه بیشتر شبیه نگهداری ثابت‌ها و event idهای logging است تا یک abstraction واقعی. بنابراین قبل از migration نیازمند تصمیم معماری درباره ماندن، ادغام یا حذف است.

## نکات مهم

- در موج اول هیچ بازطراحی عمیقی انجام نمی‌شود.
- تغییرات این مرحله صرفاً با هدف migration کنترل‌شده و آماده‌سازی نسخه 10 انجام می‌شود.
- implementation packageها در موج‌های بعدی مهاجرت خواهند کرد.# آشنایی با Zamin

## به‌روزرسانی مدیریت dependencyها

در مسیر migration به .NET 10، برای abstraction packageهای موج اول، مدیریت نسخه dependencyها به‌صورت متمرکز تنظیم شد.

در این مرحله:
- `Directory.Packages.props` برای solution abstractionها اضافه شد
- نسخه dependencyهای مشترک از فایل‌های پروژه حذف و به فایل مرکزی منتقل شد
- dependency مربوط به `Microsoft.Extensions.Logging.Abstractions` نیز برای هماهنگی با موج اول به نسخه 10 منتقل شد

## وضعیت انتشار

موج اول abstraction packageها با نسخه 10.0.0 منتشر شد.

## تکمیل migration abstractionها

در ادامه مهاجرت abstraction packageها، پروژه `Zamin.Extensions.Logger.Abstractions` نیز برای یکدستی کامل family مربوط به abstractionها به `.NET 10` مهاجرت داده شد.

این پروژه با وجود migration، همچنان deprecated محسوب می‌شود و تصمیم نهایی درباره حذف، ادغام یا بازطراحی آن به نسخه 11 موکول شده است.

## مهاجرت خانواده ObjectMappers

در ادامه migration نسخه 10، پیاده‌سازی‌های خانواده ObjectMappers نیز به `.NET 10` مهاجرت داده شدند:

- `Zamin.Extensions.ObjectMappers.AutoMapper`
- `Zamin.Extensions.ObjectMappers.Mapster`

در این مرحله dependencyهای مربوط به mapperها نیز با نسخه‌های جدید abstraction هماهنگ و مدیریت نسخه آن‌ها به‌صورت متمرکز تنظیم شد.

## مهاجرت خانواده Translations

در ادامه migration نسخه 10، پیاده‌سازی خانواده Translations نیز به `.NET 10` مهاجرت داده شد:

- `Zamin.Extensions.Translations.Parrot`

در این مرحله تمرکز بر migration و تثبیت build بود و بازطراحی معماری این بخش به مراحل بعدی موکول شد.

## مهاجرت خانواده Serializers

در ادامه migration نسخه 10، پیاده‌سازی‌های خانواده Serializers نیز به `.NET 10` مهاجرت داده شدند:

- `Zamin.Extensions.Serializers.EPPlus`
- `Zamin.Extensions.Serializers.Microsoft`
- `Zamin.Extensions.Serializers.NewtonSoft`

در این مرحله dependencyهای serializerها با abstractionهای نسخه 10 هماهنگ شدند و وابستگی‌های مربوط به ترجمه نیز با نسخه جدید سازگار شدند.

## مهاجرت خانواده Caching

در ادامه migration نسخه 10، پیاده‌سازی‌های خانواده Caching نیز به `.NET 10` مهاجرت داده شدند:

- `Zamin.Extensions.Caching.InMemory`
- `Zamin.Extensions.Caching.Distributed.Redis`
- `Zamin.Extensions.Caching.Distributed.Sql`

در این مرحله dependencyهای مربوط به cache providerها و serializerها با نسخه‌های جدید هماهنگ شدند.


## مهاجرت بخش DependencyInjection

در ادامه migration نسخه 10، packageهای زیر نیز به `.NET 10` مهاجرت داده شدند:

- `Zamin.Extensions.DependencyInjection`


در این مرحله dependencyهای این بخش‌ها با abstractionهای نسخه 10 هماهنگ شدند و build و packaging آن‌ها تثبیت شد.

## وضعیت بخش Events.Outbox

مهاجرت package `Zamin.Extensions.Events.Outbox` در این مرحله متوقف شد و به مراحل بعدی موکول گردید.

### دلیل

این package در وضعیت فعلی صرفاً یک extension مستقل نیست و برای کارکرد خود به اجزایی از Onion، به‌ویژه لایه Domain و زیرساخت داده، وابسته است. دلیل اصلی این وابستگی، استفاده از eventهای موجود در Aggregate Rootها و ثبت آن‌ها در Outbox است.

### نتیجه

مهاجرت این بخش پس از مهاجرت بخش‌های مرتبط در Onion انجام خواهد شد.

### نکته معماری

در مراحل بعدی باید مشخص شود که:
- آیا این package باید همچنان به‌عنوان بخشی از Extensions باقی بماند
- یا به‌عنوان بخشی وابسته به Onion بازتعریف و مستندسازی شود


## مهاجرت خانواده ChangeDataLog

در ادامه migration نسخه 10، packageهای خانواده `ChangeDataLog` نیز به `.NET 10` مهاجرت داده شدند:

- `Zamin.Extensions.ChangeDataLog.Hamster`
- `Zamin.Extensions.ChangeDataLog.Sql`

در این مرحله dependencyهای این بخش با abstractionهای نسخه 10 هماهنگ شدند و ساختار persistence مربوط به آن نیز با نسخه جدید سازگار شد.

## مهاجرت بخش UsersManagement

در ادامه migration نسخه 10، package `Zamin.Extensions.UsersManagement` نیز به `.NET 10` مهاجرت داده شد.

در این مرحله dependencyهای این بخش با abstraction نسخه 10 هماهنگ شدند و بخشی از dependencyهای باز مربوط به sampleهای سایر familyها نیز برطرف شد.

وابستگی باز مربوط به `Zamin.Extensions.UsersManagement` در مرحله بعدی برطرف شد و build sample امکان‌پذیر شد.
### وضعیت Sample

sample مربوط به این بخش در این مرحله به دلیل تغییرات مدل و APIهای مایکروسافت به‌صورت کامل migrate نشد.

مهاجرت package اصلی انجام شد، اما تکمیل sample به فاز بعدی و پس از بررسی دقیق‌تر تغییرات مورد نیاز موکول شد.
## وضعیت بخش MessageInbox

مهاجرت package `Zamin.Extensions.MessageBus.MessageInbox` در این مرحله متوقف شد و به مراحل بعدی موکول گردید.

### دلیل

این package در وضعیت فعلی برای کارکرد خود به بخش‌هایی از Onion وابسته است و صرفاً یک implementation مستقل برای MessageBus محسوب نمی‌شود. این وابستگی در بخش‌هایی مانند مدل‌های request/response و اجزای مرتبط با لایه‌های اصلی framework دیده می‌شود.

### نتیجه

مهاجرت این بخش پس از مهاجرت بخش‌های مرتبط در Onion انجام خواهد شد.

### نکته معماری

در مراحل بعدی باید مشخص شود که:
- آیا این package باید همچنان در خانواده Extensions باقی بماند
- یا به‌عنوان بخشی وابسته به Onion بازتعریف و مستندسازی شود