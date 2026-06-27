public class ArrayList<T> where T : IComparable<T>
{
    private T[] data;
    private int count;
    public int GetCount() => count;

    // constructor
    public ArrayList()
    {
        data = new T[0];
        count = 0;
    }

    // doubles the size of the array when it becomes full
    private void Grow()
    {   
        int newSize = data.Length == 0 ? 4 : data.Length * 2; // if the array is empty then start with size of 4. if it isn't then we double the current size
        T[] newArray = new T[newSize];

        for (int i = 0; i < count; i++)
        {
            newArray[i] = data[i];
        }
        data = newArray;
    }

    
    public T this[int index] => data[index];  //access elements in array list

    // adds an item to the front of the list
    public void AddFront(T item)
    {
        if (count == data.Length)
            Grow();
        for (int i = count; i > 0; i--)
            data[i] = data[i - 1];
        data[0] = item;
        count++;
    }

    // adds an item to the back of the list
    public void AddLast(T item)
    {
        if (count == data.Length) Grow();
        data[count] = item;
        count++;
    }

    // inserts newItem before the first occurrence of target; no-op if target not found
    public void InsertBefore(T newItem, T target)
    {
        for (int i = 0; i < count; i++)
        {
            if (data[i].CompareTo(target) == 0)
            {
                if (count == data.Length) Grow();
                for (int j = count; j > i; j--)
                    data[j] = data[j - 1];
                data[i] = newItem;
                count++;
                return;
            }
        }
    }

    // sorts all elements currently stored in the list
    public void InPlaceSort()
    {
        Array.Sort(data, 0, count);
    }

    // swaps the elements at two specified indexes
    public void Swap(int index1, int index2)
    {
        T temp = data[index1];
        data[index1] = data[index2];
        data[index2] = temp;
    }

    // removes the first element from the list
    public void DeleteFirst()
    {
        if (count == 0) return;
        for (int i = 0; i < count - 1; i++)
            data[i] = data[i + 1];
        data[count - 1] = default;
        count--;
    }

    // removes the last element from the list
    public void DeleteLast()
    {
        if (count == 0) return;
        data[count - 1] = default;
        count--;

    }

    // rotates the list left by one position
    public void RotateLeft()
    {
        if (count == 0) return;
        T first = data[0];
        for (int i = 0; i < count - 1; i++)
            data[i] = data[i + 1];
        data[count - 1] = first;
    }

    // rotates the list right by one position
    public void RotateRight()
    {
        if (count == 0) return;
        T last = data[count - 1];
        for (int i = count - 1; i > 0; i--)
            data[i] = data[i - 1];
        data[0] = last;
    }

    // creates a new list containing all elements from both lists
    public static ArrayList<T> Merge(ArrayList<T> a, ArrayList<T> b)
    {
        ArrayList<T> result = new ArrayList<T>();
        for (int i = 0; i < a.GetCount(); i++)
            result.AddLast(a.data[i]);
        for (int i = 0; i < b.GetCount(); i++)
            result.AddLast(b.data[i]);
        return result;
    }

    // returns a string containing all elements in forward order
    public string StringPrintAllForward()
    {
        string result = "";
        for (int i = 0; i < count; i++)
            result += data[i].ToString() + (i < count - 1 ? ", " : "");
        return "[" + result + "]";
    }

    // returns a string containing all elements in reverse order
    public string StringPrintAllReverse()
    {
        string result = "";
        for (int i = count - 1; i >= 0; i--)
            result += data[i].ToString() + (i > 0 ? ", " : "");
        return "[" + result + "]";
    }

    // removes all elements from the list
    public void DeleteAll()
    {
        for (int i = 0; i < count; i++)
            data[i] = default;
        count = 0;
    }
}

