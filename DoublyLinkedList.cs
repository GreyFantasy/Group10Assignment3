namespace Assignment3;
//base code form linked list lab
internal class Node<T>
{
    public T data;
    public Node<T>? next;
    public Node<T>? prev;

    public Node(T data)
    {
        this.data = data;
    }
}

public class DoublyLinkedList<T>
{
    private Node<T>? head;
    private Node<T>? tail; // For later in the lab

    public DoublyLinkedList()
    {
        head = null; // list starts empty, head = null
        tail = null; // list starts empty, tail = null
    }

    // puts out the head so other classes can traverse the list
    public Node<T>? Head { get { return head; } }


    private int count = 0;

    public int GetCount() //return count value
    {

        return count;
    }



    // Adds a new element to the front of the list.
    public void AddFirst(T toAdd)
    {
        Node<T> newNode = new Node<T>(toAdd);

        if (head == null) //checks if head is null then head and tails point to one new node, because without head list is empty
        {
            head = newNode;
            tail = newNode;
            count++; //only 1 node in list, count goes up
            return;
        }

        newNode.next = head; //point to current head (/old), old head comes after it
        head.prev = newNode; //old (/current) head point to new node, new node is before it
        head = newNode; //new head moves to new node; list head reference
        count++; //increases count on addition
    }

    // Adds a new element to the back of the list.
    public void AddLast(T toAdd)
    {

        Node<T> newNode = new Node<T>(toAdd);

        if (head == null) //checks if the list is empty at point
        {
            head = newNode; //if so then the new node becomes the firs node
            tail = newNode; // on first insertion head and tail point to same node
            count++; //count goes up
            return;
        }


        tail.next = newNode; //connects tail to new node
        newNode.prev = tail; //new node points to the node before it (old)
        tail = newNode; // moves the tail 
        count++; //count goes up
    }

    // Removes the first node from the list and returns its data.
    public T? DeleteFirst()
    {

        if (head == null)
            return default;

        T data = head.data;

        if (head.next != null) //confirm that there is more than one node in list
        {
            head = head.next; //set the head to the second node
            head.prev = null; //delete the first node (old head)
            count--; //minus count on deletion
        }

        else //if there is only one node then delete it
        {
            head = null;
            tail = null;
            count--; //minus count on deletion
        }
        return data;
    }


    // Removes the last node from the list and returns its data.
    public T? DeleteLast()
    {
        // Note: keep in mind that removing the last node requires a reference
        // to the *second* last node.

        if (tail == null) //if the list is empty then return default
            return default;

        T data = tail.data; //save the data

        if (head == tail)//if there is only one now then delete it, set head and tail to null
        {
            head = null; //head and tail are both the same node so they are both null
            tail = null;
            count--; //count decrease on deletion
        }

        else //otherwise if there is more than one node then:
        {
            tail = tail.prev; //sets tail to second last node
            tail.next = null; //delete the last node (old tail)
            count--; //count decrease on deletion

        }
        return data; //return the data
    }


    public bool Find(T toFind)
    {
        var comparer = EqualityComparer<T>.Default;

        Node<T>? curr = head;
        while (curr != null)
        {
            if (comparer.Equals(curr.data, toFind))
                return true;
            curr = curr.next;
        }

        return false;
    }

    /* ========================================================================== */
    /* ====                    Methods for the assignment                    ==== */
    /* ========================================================================== */

    // LAB NOTE: in a real world LinkedList, methods like "AddBefore" and "AddAfter"
    // would only *really* be useful if they could accept Node<T>'s as arguments, not
    // just T's (most likely they would accept either, by having a method overload).
    // (Node<T> would usually be public as well, instead of internal). That way, the
    // caller could hold onto a Node<T> reference outside of the list and re-use it
    // several times for instant O(1) insertion/removal, even if it was in the middle
    // of a very long list. With our methods as they are, however, the list must
    // first be traversed to find the node that contains 'before'/'after', making
    // both operations O(n). Swap(T, T) has a similar issue.
    //
    // That said, implementing these simpler versions is still a good learning
    // exercise because it requires you to worry about all the same edge-cases as a
    // more "correct" version. Hopefully this should keep things simpler for the
    // assignment.

    public void AddBefore(T before, T toAdd)
    {
        // e.g. calling AddBefore(E, D) on {A B C E F G} results in {A B C D E F G}.
        throw new NotImplementedException();
    }

    public void AddAfter(T after, T toAdd)
    {
        // e.g. calling AddAfter(C, D) on {A B C E F G} results in {A B C D E F G}.
        throw new NotImplementedException();
    }

    public void Swap(T element1, T element2)
    {
        // Should swap the positions of element1 and element2 in the list.
        // e.g. calling Swap(B, E) on {A B C D E F} should result in {A E C D B F}.
        throw new NotImplementedException();
    }

    public void Sort()
    {
        var comparer = Comparer<T>.Default;

        throw new NotImplementedException();
    }

    public DoublyLinkedList<T> Merge(DoublyLinkedList<T> listToAdd)
    {
        throw new NotImplementedException();
    }

    public void PrintAllNodes()
    {
        throw new NotImplementedException();
    }

    public void RotateLeft()
    {
        // e.g. calling RotateLeft on {A B C D} results in {B C D A}.
        throw new NotImplementedException();
    }

    public void RotateRight()
    {
        // e.g. calling RotateRight on {A B C D} results in {D A B C}.
        throw new NotImplementedException();
    }


    public string ToStringForward()
    {
        string s = "";

        Node<T>? curr = head; //current starts at head and move to tail
        while (curr != null)
        {
            s += curr.data?.ToString();
            if (curr.next != null)
                s += ", ";
            curr = curr.next;
        }

        return s;
    }

    public string ToStringReverse()
    {
        string s = "";

        Node<T>? curr = tail; //current starts at the tail and moves to head
        while (curr != null)
        {
            s += curr.data?.ToString();
            if (curr.prev != null) //follows the path prev
                s += ", ";
            curr = curr.prev;
        }

        return s;
    }
}
