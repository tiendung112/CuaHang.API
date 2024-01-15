using CuaHang.Common.CustomException;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CuaHang.Common
{
    public static class Utilities
    {
        private static readonly
            ConcurrentDictionary<string, string> DisplayNameCache = new ConcurrentDictionary<string, string>();

        public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize)
        {
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }

        public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize, out int totalItem)
        {
            totalItem = source.Count();
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }
        public static void PasswordValid(this string password)
        {
            var pattern = @"^\S{6,}$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(password)) throw new InvalidException(nameof(password));
        }
        public static void PhoneNumberValid(this string phoneNumber)
        {
            var pattern = @"^0[0-9]{9}$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(phoneNumber)) throw new InvalidException(nameof(phoneNumber));
        }
        public static void EmailValid(this string email)
        {
            var pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(email)) throw new InvalidException(nameof(email));
        }

        public static string HashPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool IsPasswordValid(this string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        public static void IsDefineEnum(this Type type, Enum name)
        {
            string enumName = type.GetEnumName(name);
            if (string.IsNullOrEmpty(enumName)) throw new NotFoundException(type.ToString().Substring(22));
        }
        public static string DisplayName(this Enum value)
        {
            var key = $"{value.GetType().FullName}.{value}";

            var displayName = DisplayNameCache.GetOrAdd(key, x =>
            {
                var name = (DescriptionAttribute[])value
                    .GetType()
                    .GetTypeInfo()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

                return name.Length > 0 ? name[0].Description : value.ToString();
            });

            return displayName;
        }

        public static string ConvertToUrlFriendly(string str)
        {
            str = str.ToLower();
            str = Regex.Replace(str, "/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|a/g", "a");
            str = Regex.Replace(str, "/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|e/g", "e");
            str = Regex.Replace(str, "/ì|í|ị|ỉ|ĩ|i/g", "i");
            str = Regex.Replace(str, "/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|o/g", "o");
            str = Regex.Replace(str, "/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|u/g", "u");
            str = Regex.Replace(str, "/ỳ|ý|ỵ|ỷ|ỹ|y/g", "y");
            str = str.Replace("đ", "d");
            // Some system encode vietnamese combining accent as individual utf-8 characters
            str = Regex.Replace(str, "/\u0300|\u0301|\u0303|\u0309|\u0323/g", "");
            str = Regex.Replace(str, "/\u02C6|\u0306|\u031B/g", "");
            str = string.Join('-', str.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            return str;
        }
    }

}
