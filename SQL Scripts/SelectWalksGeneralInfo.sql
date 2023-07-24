CREATE PROCEDURE SelectWalksGeneralInfo
	@imei VARCHAR(50)
AS
BEGIN
	EXECUTE dbo.GetWalks @imei;

	SELECT
		Count(Id) AS CountOfWalks,
		CAST((ISNULL(SUM(Distance), 0) / 1000) AS DECIMAL(18, 2)) AS TotalDistanceInKm,
		CAST((ISNULL(SUM(DATEDIFF(SECOND, '00:00:00', Duration)), 0) / 60.0) AS DECIMAL(18, 2)) AS TotalDurationInMin
	FROM dbo.Walk;

	DROP TABLE dbo.Walk;
END
GO