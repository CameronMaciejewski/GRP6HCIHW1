require_relative('./user.rb')
require_relative('./message.rb')

class RecordReader

  def initialize
    @user_count = 0
    @location_count = 0
    @message_count = 0
  end

  def read_msg(file)
    text = file.read(1024).unpack("C1024")
    text = text[0...text.index(0)]
    text = text.map(&:chr).join
    year = file.read(4).unpack("V")[0]
    month = file.read(4).unpack("V")[0]
    day = file.read(4).unpack("V")[0]
    hour = file.read(4).unpack("V")[0]
    minute = file.read(4).unpack("V")[0]
    message = Message.new(year, month, day, hour, minute, text)
  end

  def read_record(file)
    user = read_user file

    messages = Array.new
    num_msg = file.read(4).unpack("V")[0]
    msg = nil
    num_msg.times do 
      msg = read_msg file
      messages << msg
    end
    return user, messages
  end

  def read_user(file)
    user_id = file.read(4).unpack("V")[0]
    user_name = file.read(64).unpack("C64")
    user_name = user_name[0...user_name.index(0)]
    user_name = user_name.map(&:chr).join
    user_loc = file.read(64).unpack("C64")
    user_loc = user_loc[0...user_loc.index(0)]
    user_loc = user_loc.map(&:chr).join
    user = User.new(user_id, user_name, user_loc)
  end

end