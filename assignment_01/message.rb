class Message
  attr_accessor :year, :month, :day, :hour, :minute, :text

  def initialize(year, month, day, hour, minute, text)
    @year = year
    @month = month
    @day = day
    @hour = hour
    @minute = minute
    @text = text
  end

  def to_s
    string = "year:#{@year}\nmonth:#{@month}\nday:#{@day}\nhour:#{@hour}\nminute:#{@minute}\ntext:#{@text}"
  end
  
end