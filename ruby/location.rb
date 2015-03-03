class Location
  attr_accessor :id, :city, :state

  def initialize(id, city, state)
    @id = id
    @city = city
    @state = state
  end

  def to_s
    string = "id: #{@id}\ncity: #{@city}\nstate:#{state}"
  end

end