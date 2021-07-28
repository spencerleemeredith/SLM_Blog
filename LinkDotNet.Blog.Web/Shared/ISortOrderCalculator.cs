﻿using System.Collections.Generic;
using LinkDotNet.Domain;

namespace LinkDotNet.Blog.Web.Shared
{
    public interface ISortOrderCalculator
    {
        int GetSortOrder(ProfileInformationEntry target, IEnumerable<ProfileInformationEntry> all);
    }
}