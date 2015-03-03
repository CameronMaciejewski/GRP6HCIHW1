require_relative('./user.rb')
require_relative('./message.rb')

def readMsg(file)
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

def readRecord(file)
  user = readUser file
  puts user

  messages = Array.new
  numMsg = file.read(4).unpack("V")[0]
  numMsg.times do 
    msg = readMsg file
    puts msg
    messages << msg
  end
end

def readUser(file)
  userId = file.read(4).unpack("V")[0]
  userName = file.read(64).unpack("C64")
  userName = userName[0...userName.index(0)]
  userName = userName.map(&:chr).join
  userLoc = file.read(64).unpack("C64")
  userLoc = userLoc[0...userLoc.index(0)]
  userLoc = userLoc.map(&:chr).join
  user = User.new(userId, userName, userLoc)
end
