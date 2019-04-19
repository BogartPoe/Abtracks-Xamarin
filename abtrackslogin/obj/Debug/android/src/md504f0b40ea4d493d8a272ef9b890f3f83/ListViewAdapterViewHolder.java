package md504f0b40ea4d493d8a272ef9b890f3f83;


public class ListViewAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("abtrackslogin.ListViewAdapterViewHolder, abtrackslogin", ListViewAdapterViewHolder.class, __md_methods);
	}


	public ListViewAdapterViewHolder ()
	{
		super ();
		if (getClass () == ListViewAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("abtrackslogin.ListViewAdapterViewHolder, abtrackslogin", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
