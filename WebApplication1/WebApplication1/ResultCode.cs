using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebApplication1
{
    public enum ResultCode
    {
        Success_ALL=1,
        Account_Not_Exist,//账号已存在
        Account_Already_Exist,//账号不存在
        Password_Wrong,
        Library_Not_Exist,//所查询图书馆不存在
        LibraryID_Exist,//图书馆ID重复
        LibraryName_Exit,//图书馆名字重复
        Book_Not_Exist,//书不存在
        Book_Already_Exist,
        Book_Borrowed,
        Book_Cannot_Borrowed,
        Shelf_Not_Found,
        Shelf_Already_Exist,
        Request_Not_Found,
        Position_Not_Found,
        Book_Not_Back,
        Book_Back,
        Book_In_Back//图书已归还
    }
}
