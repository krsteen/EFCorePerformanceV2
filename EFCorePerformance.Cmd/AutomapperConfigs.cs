using AutoMapper;
using EFCorePerformance.Cmd.Dto;
using EFCorePerformance.Cmd.Model;

namespace EFCorePerformance.Cmd
{
    public class AutomapperConfigs : Profile
    {
        public AutomapperConfigs() {

            CreateMap<Report, ReportDto>();

            CreateMap<ReportDapper, ReportDto>();

            CreateMap<Report, ReportListItemDto>();



            CreateMap<ReportConfig, ReportConfigDto>();

            CreateMap<ReportConfigDapper, ReportConfigDto>();



            CreateMap<ReportComment, ReportCommentDto>();

            CreateMap<ReportCommentDapper, ReportCommentDto>();
        }

    }
}
