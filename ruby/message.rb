require('json')

class Message
  attr_accessor :year, :month, :day, :hour, :minute, :text, :user_id, :id

  def initialize(year, month, day, hour, minute, text, user_id, id)
    @year = year
    @month = month
    @day = day
    @hour = hour
    @minute = minute
    @text = text
    @user_id = user_id
    @id = id
  end

  def to_s
    string = "year:#{@year}\nmonth:#{@month}\nday:#{@day}\nhour:#{@hour}\nminute:#{@minute}\ntext:#{@text}\nuser_id:#{@user_id}\nid:#{id}"
  end

  def to_json
    json = {"year" => @year, "month" => @month, "day" => @day, "hour" => @hour, "minute" => @minute, "text" => @text, "user_id" => @user_id, "id" => @id}.to_json
  end

  def self.from_json(json)
    hash = JSON.load json
    message = Message.new(hash["year"], hash["month"], hash["day"], hash["hour"], hash["minute"], hash["text"], hash["user_id"], hash["id"])
  end

end