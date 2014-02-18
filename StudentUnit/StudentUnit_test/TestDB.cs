using DatabaseHandle;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace StudentUnit_test
{
    class TestDB
    {

        public static void createTestTable()
        {
            String sql = @"
CREATE TABLE [dbo].[T_SCS_SchoolStructure](
	[UnitID] [nvarchar](20) NOT NULL,
	[UnitName] [varchar](100) NOT NULL,
	[ParentID] [nvarchar](20) NOT NULL,
	[UnitCode] [nvarchar](20) NOT NULL,
	[StudentType] [nvarchar](10) NOT NULL,
	[ManageSystem] [nvarchar](10) NOT NULL,
	[StateData] [datetime] NULL,
	[Stata] [nvarchar](10) NOT NULL,
	[Term] [nvarchar](20) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EntranceDate] [datetime] NULL,
	[GraduateDate] [datetime] NULL,
	[DurationStudy] [int] NULL,
 CONSTRAINT [PK_T_SCS_SchoolStructure_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UnitCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [DF_T_D_SCS_SchoolStructure_FartherID]  DEFAULT ('') FOR [ParentID]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [DF_T_SCS_SchoolStructure_StudentType]  DEFAULT (N'本科生') FOR [StudentType]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [DF_T_D_SCS_SchoolStructure_ManageSystem]  DEFAULT (N'学年制') FOR [ManageSystem]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [DF_T_SCS_SchoolStructure_StateData]  DEFAULT (((1900)-(1))-(1)) FOR [StateData]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [DF_T_SCS_SchoolStructure_Stata]  DEFAULT (N'在校') FOR [Stata]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [DF_T_SCS_SchoolStructure_Term]  DEFAULT ('') FOR [Term]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [constraint_T_SCS_SchoolStructure_EntranceDate]  DEFAULT (CONVERT([datetime],CONVERT([varchar],datepart(year,getdate()),(0))+'-09-01',(0))) FOR [EntranceDate]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [constraint_T_SCS_SchoolStructure_GraduateDate]  DEFAULT (CONVERT([datetime],CONVERT([varchar],datepart(year,getdate())+(4),(0))+'-07-01',(0))) FOR [GraduateDate]
GO

ALTER TABLE [dbo].[T_SCS_SchoolStructure] ADD  CONSTRAINT [constraint_T_SCS_SchoolStructure_DurationStudy]  DEFAULT ('4') FOR [DurationStudy]
GO
";
            String[] sqlArray = Regex.Split(sql, "GO");

            foreach(String sqlitem in sqlArray)
            {
                DB.executeSQLDirect(sqlitem);
            }

        }

        public static void dropTestTable()
        {
            String sql = SQLHelper.isExistTableSQL("DROP TABLE T_SCS_SchoolStructure", "T_SCS_SchoolStructure", "");
            DB.executeSQLDirect(sql);
        }    

    }
}
