@tool
extends Node2D
class_name Map
 
@export_group("Properties")
@export 
var display_name: String

@export_range(30, 100, 1) 
var map_width :int = 30

@export_range(30, 100, 1) 
var map_height :int = 30

@export 
var music_tracks: Array[AudioStream] = []

@export_enum("safe", "unsafe", "arena", "duel", "deathmatch")
var map_type: String = "safe"

@export 
var min_level: int = 1

@export
var is_premium: bool = false

@export_enum("grass", "desert", "snow", "cave", "town")
var terrain_type: String = "grass"
@export_group("") 

@export_group("Actions")
@export_tool_button("Export to JSON", "Callable")
var export_json_btn = export_to_json

@export_tool_button("Block Edges", "Callable") 
var block_map_edges_btn = block_edges
@export_group("")




#region Tools

func export_to_json() -> void:
	var collisions = $"../Meta-Collisions" 
	var tiles = PackedInt32Array()
	
	tiles.resize(map_width * map_height)
	tiles.fill(0)
	
	for y in map_height:
		for x in map_width:
			if collisions.get_cell_source_id(Vector2i(x, y)) != -1:
				tiles[x + y * map_width] = 1
 
	var data := {
		"name": display_name,
		"width": map_width,
		"height": map_height, 
		"is_premium": is_premium,
		"min_level": min_level,
		"terrain_type": terrain_type,
		"map_type": map_type,
		"tiles": tiles
	}

	var json_text = JSON.stringify(data)
	var file_path = "res://maps/exported/%s.json" % get_parent().name
	
	var file = FileAccess.open(file_path, FileAccess.WRITE)
	file.store_string(json_text)
	file.close()


func block_edges() -> void:
	var collisions = $"../Meta-Collisions"
	  
	for y in map_height:
		for x in map_width:
			if x <= 0 || y <= 0 || y == map_height - 1 || x == map_width - 1:
				collisions.set_cell(Vector2i(x, y), 1, Vector2i(3, 0))


#endregion
