1. Set Web as startup project
2. Run
3. Open (https://localhost:44306/swagger/index.html) in a web browser. (Should open automatically)
4. Загрузите файл products_for_upload(находится в корне проекта) в эндпойнт Products/Upload
5. Задача по формированию групп напоминает собой классическую задачу о рюкзаке. Эту задачу можно решить используя Динамическое программирование(DP Memoization). Time Complexity(200 * виды_товаров * количество_товара). Я выбрал жадный алгоритм который работает за O(NlogN)
https://leetcode.com/problems/number-of-ways-to-earn-points/discuss/3258120/JavaC%2B%2BPython-Knapsack-DP
