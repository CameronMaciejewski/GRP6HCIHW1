require_relative('./user.rb')
require_relative('./message.rb')

class RecordReader

  def initialize
    @message_count = -1
  end

  def read_msg(user_id, file)
    text = file.read(1024).unpack("C1024")
    text = text[0...text.index(0)]
    text = text.map(&:chr).join
    year = file.read(4).unpack("V")[0]
    month = file.read(4).unpack("V")[0]
    day = file.read(4).unpack("V")[0]
    hour = file.read(4).unpack("V")[0]
    minute = file.read(4).unpack("V")[0]
    @message_count += 1
    message = Message.new(year, month, day, hour, minute, text, user_id, @message_count)
  end

  def read_record(file)
    user = read_user file

    messages = Array.new
    num_msg = file.read(4).unpack("V")[0]
    msg = nil
    num_msg.times do 
      msg = read_msg(user.id, file)
      messages << msg
    end
    file.close
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
    user_loc_split = user_loc.split(",")
    city = user_loc_split[0]
    state = user_loc_split[1]
    user = User.new(user_id, user_name, city, state)
  end

end