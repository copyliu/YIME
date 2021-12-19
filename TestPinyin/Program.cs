// See https://aka.ms/new-console-template for more information

using LibPinyinApi;

Console.WriteLine("Hello, World!");

using var context = new PinyinContext("data", "用户配置");
using var input = context.CreateNewInput();
input.SetInput("rufahaimeizuowan", "输");
Console.WriteLine(input.Getauxiliary());
foreach (var s in input.CandiateList)
{
    Console.Write(s+" ");
}