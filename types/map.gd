@tool
extends Node2D
class_name Map
  
@export_group("Properties")
@export var map_name: String
@export_range(30, 100, 1) var map_width
@export_range(30, 100, 1) var map_height
@export_group("")


func hello():
	print("Hello world!")
