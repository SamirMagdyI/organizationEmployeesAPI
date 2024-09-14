WITH RankedEmployees AS (
    SELECT
        EmployeeID,
        Name,
        Department,
        Salary,
        DENSE_RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS SalaryRank
    FROM
        Employees
)
SELECT
    EmployeeID,
    Name,
    Department,
    Salary
FROM
    RankedEmployees
WHERE
    SalaryRank <= 3
ORDER BY
    Department,
    Salary DESC;