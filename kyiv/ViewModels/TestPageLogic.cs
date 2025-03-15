using kyiv.Services;

using kyiv.Models;

namespace kyiv.Views
{
    public class TestPageLogic
    {
        public static int CalculateResult(out double userSum, out double totalSum, List<TestModel> testList, string[] userAnswers, string tableTopicName, int id)
        {
            totalSum = testList.Count;
            userSum = 0;

            for (int i = 0; i < testList.Count; i++)
            {
                if (userAnswers[i] != null)
                {
                    if (testList[i].CorrectAnsw.Length == 1)
                    {
                        if (userAnswers[i] == testList[i].CorrectAnsw)
                        {
                            userSum++;
                        }
                    }
                    else if (testList[i].CorrectAnsw.Length <= 3)
                    {
                        if (userAnswers[i] == testList[i].CorrectAnsw)
                        {
                            userSum++;
                        }
                        else if (userAnswers[i].Length <= testList[i].CorrectAnsw.Length)
                        {
                            foreach (var item in testList[i].CorrectAnsw)
                            {

                                if (userAnswers[i].Contains(item))
                                {
                                    userSum += 1.0 / testList[i].CorrectAnsw.Length;
                                }
                            }
                        }
                        else { }
                    }
                    else
                    {
                        if (userAnswers[i].Trim() == testList[i].CorrectAnsw.Trim())
                        {
                            userSum++;
                        }
                    }
                }

            }
            
            int ressult = (int)((userSum / totalSum) * 100);
            DbProvider.UpdateTopicTestResult(tableTopicName, id, ressult);
            return ressult;
        }
    }
}
