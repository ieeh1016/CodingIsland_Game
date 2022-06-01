using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DummyClient;

public class SessionManager
{
	static SessionManager _session = new SessionManager();
	public static SessionManager Instance { get { return _session; } }

	List<ServerSession> _sessions = new List<ServerSession>();
	object _lock = new object();


	public ServerSession Generate()
	{
		lock (_lock)
		{
			ServerSession session = new ServerSession();
			_sessions.Add(session);
			return session;
		}
	}
}