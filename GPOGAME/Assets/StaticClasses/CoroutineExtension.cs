using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class CoroutineExtension
{
    static private readonly Dictionary<string, int> Runners = new Dictionary<string, int>();

    // MonoBehaviour ��� ����� ��� ������� �������� � ��������� ����������� ������
    public static void ParallelCoroutinesGroup(this IEnumerator coroutine, MonoBehaviour parent, string groupName)
    {
        if (!Runners.ContainsKey(groupName))
            Runners.Add(groupName, 0);

        Runners[groupName]++;
        parent.StartCoroutine(DoParallel(coroutine, parent, groupName));
    }


    static IEnumerator DoParallel(IEnumerator coroutine, MonoBehaviour parent, string groupName)
    {
        yield return parent.StartCoroutine(coroutine);
        Runners[groupName]--;
    }

    // ��� ������� ����������, ��� �� ������, ���� �� � ������ ������������� ��������
    public static bool GroupProcessing(string groupName)
    {
        return (Runners.ContainsKey(groupName) && Runners[groupName] > 0);
    }
}

