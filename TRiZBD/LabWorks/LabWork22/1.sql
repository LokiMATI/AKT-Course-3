UPDATE Import1_Users
SET lastenter =  CASE 
 WHEN CHARINDEX('/', lastenter) > 0 THEN 
 FORMAT(TRY_CONVERT(DATE, lastenter, 101), 'dd.MM.yyyy') -- MM/dd/yyyy 
 ELSE 
 lastenter END