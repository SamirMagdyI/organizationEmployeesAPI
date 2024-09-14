-- This SQL query retrieves the top 3 highest-paid employees from each department.

-- Approach:
-- 1. We use a Common Table Expression (CTE) named RankedEmployees to rank employees within each department based on their salary.
-- 2. The DENSE_RANK() function is used to assign ranks, allowing for ties (i.e., if two employees have the same salary, they get the same rank).
-- 3. We partition the ranking by Department and order by Salary in descending order.
-- 4. In the main query, we select from the CTE and filter for employees with a rank of 3 or less.
-- 5. The result is ordered by Department and then by Salary in descending order.

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

-- This query efficiently handles the requirement to find the top 3 highest-paid employees
-- in each department, even in cases where there might be salary ties.