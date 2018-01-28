using TypeCast.Attributes;
using TypeCast.ContentTypes;
using TypeCast.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypeCast.TestTarget.TypeSet3
{
    [MemberGroup]
    public class GroupFace : MemberGroupBase { }

    [MemberType]
    public class TestMember : MemberTypeBase
    {
        [MemberProperty(memberCanEdit: true, showOnProfile: true)]
        public virtual Textstring PhoneNumber { get; set; }

        [MemberProperty(memberCanEdit: true, showOnProfile: false)]
        public MemberPreferences Preferences { get; set; }
    }

    [EnumDataType]
    [Flags]
    public enum MemberPreferences
    {
        None = 0,
        ReceiveEmail = 1,
        ReceiveMarketingEmail = 2,
        UseTwoFactorViaSMS = 4
    }
}