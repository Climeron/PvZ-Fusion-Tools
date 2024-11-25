using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClimeronToolsForPvZ.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>Аналог ForEach (System.Collections.Generic) для работы с коллекциями любого типа.</summary>
        /// <typeparam name="TSource">Тип элементов source.</typeparam>
        /// <param name="source">Последовательность значений, для которых вызывается действие.</param>
        /// <param name="action">Делегат Action«TSource», выполняемый для каждого элемента коллекции IEnumerable«TSource».</param>
        /// <returns>Исходная коллекция.</returns>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (TSource item in source)
                action(item);
        }
        /// <summary>Аналог ForEach (System.Collections.Generic) с возвратом исходной коллекции.</summary>
        /// <typeparam name="TSource">Тип элементов source.</typeparam>
        /// <param name="source">Последовательность значений, для которых вызывается действие.</param>
        /// <param name="action">Делегат Action«TSource», выполняемый для каждого элемента коллекции IEnumerable«TSource».</param>
        /// <returns>Исходная коллекция.</returns>
        public static IEnumerable<TSource> ForEachExt<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (TSource item in source)
            {
                action(item);
                yield return item;
            }
        }
        /// <summary>Аналог FindAll (System.Collections.Generic).</summary>
        /// <typeparam name="TSource">Тип элементов source.</typeparam>
        /// <param name="source">Последовательность значений, для которых вызывается действие.</param>
        /// <param name="match">Делегат Predicate«TSource», определяющий условия поиска элементов.</param>
        /// <returns>Коллекция IEnumerable«TSource», содержащая все элементы, удовлетворяющие условиям указанного предиката, если такие элементы найдены. В противном случае — пустая коллекция IEnumerable«TSource».</returns>
        public static IEnumerable<TSource> FindAll<TSource>(this IEnumerable<TSource> source, Predicate<TSource> match)
        {
            foreach (TSource item in source)
                if (match(item))
                    yield return item;
        }
        /// <summary>Объединяет элементы последовательности в строку с чередующимися разделителями.</summary>
        /// <typeparam name="TSource">Тип элементов values.</typeparam>
        /// <param name="values">Последовательность, содержащая строки для сцепления.</param>
        /// <param name="separatorsArray">Строки для использования в качестве разделителя.</param>
        /// <returns>Строка, состоящая из элементов values, поочерёдно разделяемых строками separatorsArray.</returns>
        public static string Join<TSource>(this IEnumerable<TSource> values, params string[] separatorsArray)
        {
            if (separatorsArray.Length == 0)
                return values.Join("");
            List<TSource> valuesList = values.ToList();
            if (valuesList.Count == 0)
                return string.Empty;
            StringBuilder stringBuilder = new(valuesList[0]?.ToString() ?? "NULL");
            int separatorIndex = 0;
            for (int i = 1; i < valuesList.Count; i++)
            {
                stringBuilder.Append(separatorsArray[separatorIndex]);
                stringBuilder.Append(valuesList[i]);
                separatorIndex = (separatorIndex + 1) % separatorsArray.Length;
            }
            return stringBuilder.ToString();
        }
        /// <summary>Объединяет две последовательности путем чередования элементов. Если одна последовательность длиннее другой, оставшиеся элементы добавляются в конец результирующей последовательности.</summary>
        /// <typeparam name="TSource">Тип элементов source.</typeparam>
        /// <param name="source">Первая последовательность элементов.</param>
        /// <param name="mergingCollection">Вторая последовательность элементов.</param>
        /// <returns>Попарно соединённая последовательность.</returns>
        public static IEnumerable<TSource> PairMerge<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> mergingCollection)
        {
            List<TSource> sourceStack = new(source);
            List<TSource> mergingCollectionStack = new(mergingCollection);
            int maxSize = Math.Max(sourceStack.Count, mergingCollectionStack.Count);
            for (int i = 0; i < maxSize; i++)
            {
                if (sourceStack.TryDequeue(out TSource sourceItem))
                    yield return sourceItem;
                if (mergingCollectionStack.TryDequeue(out TSource mergingCollectionItem))
                    yield return mergingCollectionItem;
            }
        }
        /// <summary>Преобразует последовательность в строку вида "нумерованный список".</summary>
        /// <param name="values">Последовательность, содержащая строки для сцепления.</param>
        /// <returns>Строка, состоящая из элементов values, разнесённых построчно и пронумерованных.</returns>
        public static string ToNumberedFormat(this IEnumerable values)
        {
            StringBuilder stringBuilder = new();
            int n = 0;
            foreach (object item in values)
            {
                if (n > 0)
                    stringBuilder.Append('\n');
                stringBuilder.Append($"{n++}: {item}");
            }
            return stringBuilder.ToString();
        }
        /// <summary>Удаляет и возвращает первый элемент списка, имитируя поведение очереди.</summary>
        /// <typeparam name="T">Тип элементов списка.</typeparam>
        /// <param name="list">Список, из которого удаляется элемент.</param>
        /// <returns>Первый элемент списка.</returns>
        /// <exception cref="InvalidOperationException">Если список пуст.</exception>
        public static T Dequeue<T>(this List<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new InvalidOperationException("List is empty.");
            T item = list[0];
            list.RemoveAt(0);
            return item;
        }
        /// <summary>Пытается удалить и вернуть первый элемент списка, имитируя поведение очереди.</summary>
        /// <typeparam name="T">Тип элементов списка.</typeparam>
        /// <param name="list">Список, из которого удаляется элемент.</param>
        /// <param name="item">Первый элемент списка, если он есть.</param>
        /// <returns>Возвращает <c>true</c>, если элемент успешно удален и возвращен; иначе <c>false</c>.</returns>
        public static bool TryDequeue<T>(this List<T> list, out T item)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
            {
                item = default;
                return false;
            }
            item = list.Dequeue();
            return true;
        }
    }
}
