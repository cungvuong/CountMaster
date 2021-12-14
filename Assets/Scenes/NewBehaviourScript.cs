using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounter
{
	public class FenwickTree
	{
		int[] array;
		public FenwickTree(int size = 10000)
		{
			size++;
			array = new int[size];
			for (int i = 0; i < size; ++i)
				array[i] = 0;
		}
		public void Update(int x, int v)
		{
			x++;
			while (x < array.Length)
			{
				array[x] += v;
				x += x & (-x);
			}
		}
		public int Get(int x)
		{
			x++;
			int sum = 0;
			while (x > 0)
			{
				sum += array[x];
				x -= x & (-x);
			}
			return sum;
		}
	}
	FenwickTree f;
	bool[] array;
	public PlayerCounter(int size, bool initialState)
	{
		f = new FenwickTree(size);
		array = new bool[size];
		for (int i = 0; i < size; ++i)
		{
			array[i] = initialState;
			if (initialState)
				f.Update(i, 1);
		}
	}
	public void SetAlive(int index)
	{
		if (!array[index])
		{
			f.Update(index, 1);
		}
	}
	public void SetDead(int index)
	{
		if (array[index])
		{
			f.Update(index, -1);
		}
	}
	public int Count(int zeroToIndexExclusive)
	{
		return f.Get(zeroToIndexExclusive - 1);
	}
}