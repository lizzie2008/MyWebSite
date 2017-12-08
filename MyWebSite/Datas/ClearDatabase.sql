--/第1步**********删除所有表的外键约束*************************/
 
DECLARE c1 cursor for
select 'alter table ['+ object_name(parent_obj) + '] drop constraint ['+name+']; '
from sysobjects
where xtype = 'F'
open c1
declare @c1 varchar(8000)
fetch next from c1 into @c1
while(@@fetch_status=0)
begin
exec(@c1)
fetch next from c1 into @c1
end
close c1
deallocate c1
 
--/第2步**********删除所有表*************************/
 
use MyWebSite
GO
declare @sql varchar(8000)
while (select count(*) from sysobjects where type='U')>0
begin
SELECT @sql='drop table ' + name
FROM sysobjects
WHERE (type = 'U')
ORDER BY 'drop table ' + name
exec(@sql)
end