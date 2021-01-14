﻿using EFCorePerformance.Cmd.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePerformance.Cmd.Service
{
    public class ReportServiceEfRawBetterIndexes : ReportServiceBase, IReportService
    {
        public ReportServiceEfRawBetterIndexes()
            : base()
        {
        }

        public async Task<ReportResponse> GetReportByIdAsync(int id)
        {
            var query = $"SELECT * FROM [dbo].[ReportsWithBetterIndex] r WHERE r.[IsArchived] = 0 AND r.[ReportId] = @ReportId" ;

            var report = await Db.ReportsWithBetterIndex
                            .FromSqlRaw(query, new SqlParameter("ReportId", id))
                            .TagWith(QueryTag("Report by Id"))
                            .Include(r => r.Config)
                            .Include(r => r.Comments)
                            .FirstOrDefaultAsync();

            var result = new ReportResponse(1, Serialize(report));

            return result;
        }       

        public async Task<ReportResponse> GetDetailedReportListAsync(string nameLike = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ReportResponse> GetLightReportListAsync(string nameLike = null)
        {
            var query = $"SELECT * FROM [dbo].[ReportsWithBetterIndex] r";
            query = nameLike == null ? AddArchivedWhere(query) : AddNameWhere(query);
            query = AddPaging(query, Constants.DEFAULT_SKIP, Constants.DEFAULT_TAKE);

            var reports = await Db.ReportsWithBetterIndex
                .FromSqlRaw(query, String.IsNullOrWhiteSpace(nameLike) ? null : new SqlParameter("Name", nameLike))
                .TagWith(QueryTag("Report list light"))
                .ToListAsync();

            var result = new ReportResponse(reports.Count(), Serialize(reports));

            return result;
        }

        string AddPaging(string baseQuery, int skip, int take)
        {
            return baseQuery += $" ORDER BY r.[ReportId] OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
        }

        string AddArchivedWhere(string baseQuery)
        {
            return baseQuery += " WHERE r.[IsArchived] = 0";
        }

        string AddNameWhere(string baseQuery)
        {
            return baseQuery += $" WHERE r.[IsArchived] = 0 AND r.[Name] = @Name";
        }
    }
}