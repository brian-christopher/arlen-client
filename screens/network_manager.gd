extends Node

signal received_message(message: String)

var socket: WebSocketPeer = WebSocketPeer.new()

func connect_to_host(address:String, port:int) -> void:
	socket.connect_to_url("ws://%s:%d" % [address, port])


func send_message(message: String) -> void:
	if socket.get_ready_state() == WebSocketPeer.STATE_OPEN && !message.is_empty():
		socket.send_text(message)


func _process(_delta: float) -> void:
	socket.poll()
	var state = socket.get_ready_state()
	if state == WebSocketPeer.STATE_OPEN:
		while socket.get_available_packet_count():
			var packet = socket.get_packet()
			received_message.emit(packet.get_string_from_utf8())
	elif state == WebSocketPeer.STATE_CLOSING:
		# Keep polling to achieve proper close.
		pass
	elif state == WebSocketPeer.STATE_CLOSED:
		var code = socket.get_close_code()
		var reason = socket.get_close_reason()
		print("WebSocket closed with code: %d, reason %s. Clean: %s" % [code, reason, code != -1])
		set_process(false) # Stop processing.
