class User
  attr_accessor :id, :name, :location

  def initialize(id, name, location)
    @id = id
    @name = name
    @location = location
  end

  def to_s
    string = "id: #{@id}\nname: #{@name}\nlocation:#{location}"
  end

end