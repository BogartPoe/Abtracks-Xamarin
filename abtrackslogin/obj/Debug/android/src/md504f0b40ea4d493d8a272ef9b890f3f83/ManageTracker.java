package md504f0b40ea4d493d8a272ef9b890f3f83;


public class ManageTracker
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("abtrackslogin.ManageTracker, abtrackslogin", ManageTracker.class, __md_methods);
	}


	public ManageTracker ()
	{
		super ();
		if (getClass () == ManageTracker.class)
			mono.android.TypeManager.Activate ("abtrackslogin.ManageTracker, abtrackslogin", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
