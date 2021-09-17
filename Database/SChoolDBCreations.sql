Create DataBase School ;
CREATE TABLE Classes (
    ClassID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    ClassName varchar(255) 
);
CREATE TABLE Teachers (
    TeacherID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    TeacherName varchar(255) 
);

CREATE TABLE ClassSession (
    SessionID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    SessionName varchar(255) ,
	ClassID UNIQUEIDENTIFIER ,
	TeacherID UNIQUEIDENTIFIER,
	FOREIGN KEY (ClassID) REFERENCES Classes(ClassID),
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID),
	SessionStartTime Datetime ,
	SessionEndTime Datetime 
);


INSERT INTO Classes (ClassID, ClassName)
VALUES (NewID(), 'CS'),
 (NewID(), 'IT'),
 (NewID(), 'DS'),
  (NewID(), 'IS');

  INSERT INTO Teachers(TeacherID, TeacherName)
  VALUES (NewID(), 'Abdel rahman'),
  (NewID(), 'Ahmed Hassan'),
  (NewID(), 'Abdel Satar '),
  (NEWID(), 'Hassan');
  select * from Classes;
  select * from Teachers;

  INSERT INTO ClassSession (SessionID, SessionName,ClassID,TeacherID,SessionStartTime,SessionEndTime)
VALUES (NewID(), 'Network','E4EE5E09-337B-4D60-8408-506DB61B5F4E','DABE412C-522D-4FD3-84DB-654485BB5769','5/29/2020 9:00:00 AM','5/29/2020 10:00:00 AM'),
(NewID(), 'C#','EDAA93FE-D894-426E-92AC-AFEA2EB653D6','B1D59CA4-B645-48EC-B3D7-AC6507639E2A','5/29/2020 11:00:00 AM','5/29/2020 1:00:00 PM')
  ;
SELECT  TeacherName,ClassName ,
        MONTH(SessionStartTime) as month,
Year(SessionStartTime) as Year,
       sum( cast(DATEDIFF(HOUR,cast(SessionStartTime as time),cast(SessionEndTime as time ))as float))  as WorkingHour
FROM classsession
inner join Teachers on ClassSession.TeacherID = Teachers.TeacherID
inner join Classes on ClassSession.ClassID = Classes.ClassID
GROUP BY  TeacherName,ClassName,
          MONTH(SessionStartTime),
 Year(SessionStartTime)

order by TeacherName,
 Year(SessionStartTime)


 SELECT  ClassName ,
        day(SessionStartTime) as day,
        MONTH(SessionStartTime) as month,
Year(SessionStartTime) as Year,
      6 - sum( cast(DATEDIFF(HOUR,cast(SessionStartTime as time),cast(SessionStartTime as time ))as float))  as ClassFreeTime
FROM classsession
inner join Classes on ClassSession.ClassID = Classes.ClassID
GROUP BY  ClassName,
day(SessionStartTime),
          MONTH(SessionStartTime),
 Year(SessionStartTime)

order by ClassName,
 day(SessionStartTime),
          MONTH(SessionStartTime),
 Year(SessionStartTime)

