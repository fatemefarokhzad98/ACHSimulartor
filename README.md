# مستند راهنمای سیستم انتقال وجه شبیه به پایا

## پیش نیاز ها :
**تکنولوژی:** استفاده از ASP.NET Core 7.0 یا نسخه‌های بالاتر الزامی است.

**نگهداری داده:** برای ذخیره در دیتابیس از mssql استفاده کنید .

**کامیت ها:** ترتیب و مسج کامیت ها مهم هستند 

**معماری:** استفاده از Clean Architecture برای ساختار پروژه به‌عنوان یک نکته مثبت توصیه می‌شود. 


**تحویل تسک:** لطفا یک پروژه private بسازید و این ریپوزیتوری رو clone کنید و *@amirabbas-ranjbar*  رو به پروژه اضافه کنید . بعد از اتمام تسک یک مرج ریکوئست به master باز کنید و لینک merge request  رو ارسال کنید .





## توضیحات سیستم:

این سیستم یک شبیه‌سازی از پایا است که کاربران می‌توانند درخواست انتقال وجه خود را با ارسال یک دستور از طریق ورودی ثبت کنند. بعد از ثبت درخواست، مبلغ از حساب کاربر کسر شده و نزد بانک رزرو می‌شود. در این مرحله یک تراکنش کسر وجه در حساب کاربر ثبت خواهد شد. شماره شبا باید با پیشوند IR شروع شود و به طول 24 رقم باشد.


#### ارسال درخواست انتقال وجه:

#### درخواست:

```
POST http://localhost:80/api/sheba
Accept: application/json
```

قالب داده:
```json
{
    "price": 200000000,
    "fromShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
    "ToShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
    "note": "توضیحات تراکنش"
}
```
پاسخ در صورت خطا:
```json
{
    "message": "Error Message",
    "code": "ErrorCode"
}
```
پاسخ در صورت موفقیت:
```json
{
    "message": "Request is saved successfully and is in pending status",
    "request": {
        "id": "request-id",
        "price": 200000000,
        "status": "pending",
        "fromShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
        "ToShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
        "createdAt": "2024-02-10T12:30-02:00"
    }
}
```
----------------------------

#### بررسی درخواست‌ها توسط اپراتور:

درخواست‌های کاربران در یک لیست مرتب بر اساس زمان ثبت قرار می‌گیرند. اپراتور می‌تواند با فراخوانی API لیست درخواست‌ها را مشاهده کرده و سپس با بررسی آدرس مبدا، مقصد و مبلغ تراکنش درخواست را تایید یا رد کند.

در صورت رد شدن درخواست، پول به حساب کاربر بازگردانده شده و یک تراکنش بازگشت وجه ثبت خواهد شد.

-----------------------------

#### تایید یا لغو درخواست انتقال وجه:
```
PUT/POST http://localhost:80/api/sheba/{{request-id}}
Accept: application/json
Content-Type: application/json
```
قالب داده:
```json
{
    "status": "confirmed", // یا canceled
    "note": "توضیحات تراکنش" // در سناریوی لغو درخواست
}
```
پاسخ در صورت خطا:
```json
{
    "message": "Error Message",
    "code": "ErrorCode"
}
```
پاسخ در صورت موفقیت:
```json
{
    "message": "Request is Confirmed!", // یا Canceled
    "request": {
        "id": "request-id",
        "price": 200000000,
        "status": "confirmed", // یا canceled
        "fromShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
        "ToShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
        "createdAt": "2024-02-10T12:30-02:00"
    }
}
```
------------------------------
#### مشاهده لیست درخواست‌ها:
```
GET http://localhost:80/api/sheba
Accept: application/json
```

پاسخ موفق:
```json
{
    "requests": [
        {
            "id": "request-id",
            "price": 200000000,
            "status": "confirmed", // یا canceled
            "fromShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
            "ToShebaNumber": "IRxxxxxxxxxxxxxxxxxxxxx",
            "createdAt": "2024-02-10T12:30-02:00"
        }
    ]
}
```

## نکات مهم

**بررسی درخواست:** درخواست‌های کاربران در هر مرحله باید بررسی شده و در صورت بروز مشکل خطای مناسب بازگردانده شود.

**محدودیت مبلغ:** مبلغ درخواست نباید بیشتر از موجودی قابل برداشت کاربر باشد.

**عدم نیاز به احراز هویت:** نیازی به پیاده‌سازی احراز هویت کاربر نیست.

**ثبت تراکنش:** هنگام ثبت درخواست یا تراکنش با موفقیت ثبت می‌شود و مبلغ کسر می‌گردد یا در صورت بروز خطا هیچ تغییری اعمال نمی‌شود.

**صف بررسی:** صف بررسی کارمند بانک باید بر اساس زمان درخواست باشد، به شکلی که درخواست‌های زودتر در بالای لیست قرار بگیرند.

# ACHSimulartor
