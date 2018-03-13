//---------------------------------------------------------------------
// <copyright file="PluralizationService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// @owner       venkatja
// @backupOwner willa
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Voodoo.CodeGeneration.Pluralizer
{
    public abstract class PluralizationService
    {
        public CultureInfo Culture { get; protected set; }

        public abstract bool IsPlural(string word);
        public abstract bool IsSingular(string word);
        public abstract string Pluralize(string word);
        public abstract string Singularize(string word);
     
        public static PluralizationService CreateService(CultureInfo culture)
        {
            return new EnglishPluralizationService();
        }
    }
}