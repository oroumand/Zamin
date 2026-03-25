# راهبرد Build و Packaging

## نمای کلی

در Zamin برای مدیریت build و packaging از ساختار چندلایه مبتنی بر MSBuild استفاده می‌شود.

این ساختار شامل دو سطح اصلی است:

- تنظیمات سراسری در ریشه repository
- تنظیمات محلی برای برخی solutionها یا گروه‌های خاص از packageها

## تنظیمات سراسری

در ریشه repository فایل‌های زیر قرار دارند:

- `global.json`
- `Directory.Build.props`
- `Directory.Packages.props`

### global.json
این فایل نسخه SDK مورد استفاده برای build را مشخص می‌کند تا build روی سیستم‌های مختلف با نسخه مشخص و یکسان انجام شود.

### Directory.Build.props
این فایل تنظیمات عمومی build و metadataهای مشترک را نگهداری می‌کند، از جمله:

- Nullable
- ImplicitUsings
- GenerateDocumentationFile
- Authors
- Company
- RepositoryUrl
- Package metadataهای مشترک
- Package icon

### Directory.Packages.props
این فایل برای مدیریت متمرکز نسخه packageهای وابسته استفاده می‌شود. در حال حاضر به عنوان زیرساخت اولیه اضافه شده و در مراحل بعدی تکمیل خواهد شد.

## تنظیمات محلی

برای migration مرحله‌ای، در برخی solutionها یک `Directory.Build.props` محلی نیز تعریف شده است.

برای مثال در solution مربوط به `Zamin.Extensions.Abstractions` این فایل محلی وظیفه دارد:

- TargetFramework را روی `net10.0` تنظیم کند
- Version را روی `10.0.0` قرار دهد
- `GeneratePackageOnBuild` را فعال کند

از آنجا که MSBuild پس از پیدا کردن اولین `Directory.Build.props` به طور پیش‌فرض جستجو را متوقف می‌کند، فایل محلی به‌صورت صریح فایل ریشه را import می‌کند تا تنظیمات عمومی از بین نروند.

## Packaging

در موج اول migration، packageهای abstraction به گونه‌ای تنظیم شده‌اند که در زمان build، package نیز تولید شود.

این رفتار با property زیر فعال شده است:

- `GeneratePackageOnBuild = true`

با این حال برای سناریوهای release و انتشار دسته‌ای، استفاده از دستورهای صریح `pack` همچنان گزینه قابل اتکاتری است.

## نتیجه

این ساختار باعث می‌شود:

- تنظیمات مشترک به‌صورت مرکزی مدیریت شوند
- migration به‌صورت موجی و کنترل‌شده انجام شود
- metadataها و رفتار build بین packageها یکدست بماند

## مدیریت متمرکز نسخه پکیج‌ها

برای کاهش پراکندگی نسخه dependencyها و ساده‌سازی migration، در solution مربوط به abstractionها از Central Package Management استفاده شده است.

در این روش:

- نسخه dependencyها در فایل `Directory.Packages.props` تعریف می‌شود
- در فایل‌های csproj فقط `PackageReference` بدون نسخه نگه داشته می‌شود
- این کار باعث یکدست شدن dependencyها و ساده‌تر شدن به‌روزرسانی نسخه‌ها می‌شود

در موج اول، این الگو ابتدا برای solution مربوط به `Zamin.Extensions.Abstractions` فعال شده است.