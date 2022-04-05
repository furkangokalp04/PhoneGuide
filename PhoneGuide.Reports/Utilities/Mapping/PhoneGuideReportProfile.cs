using AutoMapper;
using PhoneGuide.Reports.Entities;
using PhoneGuide.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneGuide.Reports.Utilities.Mapping
{
    public class PhoneGuideReportProfile:Profile
    {
        public PhoneGuideReportProfile()
        {
            CreateMap<ReportDto, Report>().ReverseMap();
        }
    }
}
