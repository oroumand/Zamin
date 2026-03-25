# معماری فعلی Zamin

## نمای کلی
Zamin یک framework چندبخشی در اکوسیستم .NET است که به مرور از یک ابزار توسعه داخلی به یک بستر مشترک برای چند تیم تبدیل شده است. ساختار فعلی آن از سه ناحیه اصلی تشکیل شده است:

- Onion
- Extensions
- Root Utilities

در کنار این‌ها، یک ناحیه utility داخلی نیز در `Onion/src/1.Utilities` وجود دارد که باید از `Root Utilities` تفکیک مفهومی داشته باشد.

## Onion
بخش Onion هسته معماری framework را تشکیل می‌دهد و شامل اجزای پایه برای توسعه سیستم‌هایی با معماری Onion است. این بخش شامل لایه‌های دامنه، application services، قراردادها، request/response، زیرساخت داده مبتنی بر SQL و endpointهای وب است.

در این ناحیه، پروژه `Zamin.Utilities` در مسیر `Onion/src/1.Utilities` قرار دارد که شامل utilityهای پایه مانند تاریخ، زمان، تبدیل انواع و extension methodهای عمومی است.

## Extensions
بخش Extensions شامل دو لایه اصلی است:

### 1. Abstractions Family
این بخش مجموعه‌ای از packageهای contract-first است که برای قابلیت‌هایی مانند caching، dependency injection، events، message bus، object mapping، serialization، translation و user management abstraction فراهم می‌کنند.

### 2. Implementation Families
در این بخش، برای هر abstraction یک یا چند implementation ارائه شده است. اغلب implementationها از الگوی زیر پیروی می‌کنند:

- وابستگی به abstraction متناظر
- وجود registration در `ServiceCollectionExtensions`
- وجود sample project در برخی familyها
- استفاده از کتابخانه‌ها یا سرویس‌های بیرونی مانند Redis، RabbitMQ، AutoMapper، Mapster، Newtonsoft، EPPlus و غیره

در برخی familyها، یک الگوی تکرارشونده قابل مشاهده است:
- abstraction
- implementation
- data access layer
- external dependency

نمونه‌هایی از این الگو در ChangeDataLog، Events، MessageBus و Translations دیده می‌شود.

## Root Utilities
بخش Root Utilities شامل مجموعه‌ای از registrationها، logging infrastructureها و capabilityهای مستقل است. این بخش یکدست نیست و پروژه‌های آن در چند دسته مفهومی قرار می‌گیرند:

- registrationهای ساده مانند OpenTelemetryRegistration و ScalarRegistration
- زیرساخت logging مانند SerilogRegistration و Elasticsearch sink
- قابلیت‌های مستقل مانند SoftwarepartDetector
- قابلیت‌های نیازمند بازبینی جدی مانند Auth.ApiAuthentication

## ویژگی‌های معماری فعلی
- solutionها به صورت مستقل نگهداری می‌شوند.
- packageها و folderها در اغلب موارد هم‌نام هستند.
- انتشار packageها به صورت دستی انجام می‌شود.
- تست خودکار قابل توجهی در وضعیت فعلی وجود ندارد.
- برای بخش‌های مختلف framework، sampleهای متعددی وجود دارد، اما sample اصلی framework هنوز کامل و مرجع onboarding نیست.
- برخی وابستگی‌های بین familyهای مختلف Extensions وجود دارد که بخشی از آن‌ها طبیعی و بخشی نیازمند بازبینی است.
- بعضی بخش‌ها مانند MessageBus، Translations، Auth و برخی Utilities نیازمند بازطراحی یا حداقل بازبینی جدی هستند.

## جمع‌بندی
معماری فعلی Zamin حاصل رشد تدریجی و عملی framework در طول زمان است. این معماری دارای نقاط قوت مهمی مانند تفکیک abstraction از implementation، وجود adapterهای متعدد، و هسته Onion نسبتاً مشخص است؛ اما در کنار آن، چالش‌هایی مانند ناهماهنگی در برخی مرزها، نبود تست کافی، رشد نامتوازن برخی Utilities و پیچیدگی بعضی familyها نیز دیده می‌شود.

هدف فازهای بعدی، ارتقا به .NET 10، تمیزسازی معماری، تثبیت public contractها، تکمیل sample اصلی و مستندسازی کامل framework خواهد بود.