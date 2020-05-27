alter PROCEDURE Promote @Studies varchar(100), @Semester INT
AS
BEGIN
    Declare @IdStudies INT = (SELECT IdStudy FROM Studies WHERE Studies.Name = @Studies);
    IF @IdStudies IS NULL
    BEGIN
    	RAISERROR('No study found',16,1)
    END
    
    DECLARE @CurrentIdEnrollment INT = (SELECT IdEnrollment from Enrollment WHERE IdStudy = @IdStudies AND Semester =@Semester);
    DECLARE @IdNextEnrollment INT = (SELECT IdEnrollment from Enrollment WHERE IdStudy = @IdStudies AND Semester =@Semester +1);
	DECLARE @IdEnrollmentTemp INT = (SELECT RAND()*(1000-20)+20);
    IF @IdNextEnrollment IS NULL
    BEGIN
    	INSERT INTO ENROLLMENT VALUES (@IdEnrollmentTemp ,@Semester + 1 , @IdStudies, SYSDATETIME());
		UPDATE Student SET IdEnrollment = @IdEnrollmentTemp Where IdEnrollment = @CurrentIdEnrollment;
    END
    ELSE
    BEGIN
    	UPDATE Student SET IdEnrollment = @IdNextEnrollment Where IdEnrollment = @CurrentIdEnrollment;
	END
END