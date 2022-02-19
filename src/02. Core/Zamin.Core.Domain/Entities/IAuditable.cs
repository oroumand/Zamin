namespace Zamin.Core.Domain.Entities;
/// <summary>
/// هنگام ذخیره و بازیابی از دیتابیس نیاز است فیلد‌های خاصی برای شناسایی فرایند و روال تغییر ویژگی‌های Entity در دیتابیس ذخیره شود.
/// این فیلد‌ها برای فرایند Auditing ضروری می‌باشند.
/// به طور معمول فیلد‌های زیر به جدول اضافه می‌شود
/// CreatedByUserId
/// ModifiedByUserId
/// CreatedDateTime
/// ModifiedDateTime
/// </summary>
public interface IAuditable
{
}